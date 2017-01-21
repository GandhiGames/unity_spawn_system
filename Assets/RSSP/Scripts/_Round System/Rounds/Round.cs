using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Responsible for storing, managing, and updating an array of Checkpoint. Handles round preperation time (if #HasPreperationTime), preperation object spawning,
	/// and boss spawning (if #RoundHasBoss).
	/// </summary>
	[System.Serializable]
	public class Round
	{
		#region Checkpoints
		/// <summary>
		/// Array of checkpoints owned by this round.
		/// </summary>
		public RoundCheckpoint[] Checkpoints;
		
		private int _currentCheckpoint = -1;
		/// <summary>
		/// Gets the current checkpoint.
		/// </summary>
		/// <value>The current Checkpoint.</value>
		public RoundCheckpoint CurrentCheckpoint {
			get {
				if (_currentCheckpoint != -1) {
					return Checkpoints [_currentCheckpoint];
				}
				
				return null;
			}
		}
		#endregion
		
		#region Round Preperation
		/// <summary>
		/// A preperation time to allow for the player to prepare for the upcoming round.
		/// </summary>
		public bool HasPreperationTime = true;
		
		/// <summary>
		/// Preperation time in seconds.
		/// </summary>
		public int PreperationTime = 10;
		
		/// <summary>
		/// Can spawn objects during preperaion time.
		/// </summary>
		public bool SpawnObjectsDuringPreperaionTime = true;
		
		/// <summary>
		/// The number of objects to spawn. This number will be 
		/// spawned as long as #PreperationTimeObjects is geater than one.
		/// </summary>
		public int NumberOfObjectsToSpawn;
		
		/// <summary>
		/// The preperation time objects.
		/// </summary>
		public RoundObjectSpawner[] PreperationTimeObjects;

		/// <summary>
		/// If true, DestroyCurrentEnemiesRequestEvent is raised on round start. 
		/// </summary>
		public bool DestroyPreviousRoundEnemiesOnRoundStart;
		
		/// <summary>
		/// If true, DestroyCurrentEnemiesRequestEvent is raised on round end. 
		/// </summary>
		public bool DestroyEnemiesOnRoundEnd;
		
		private bool _inPreparation = true;
		/// <summary>
		/// Gets a value indicating whether this <see cref="RoundManager.Round"/> is in the preperation stage.
		/// </summary>
		/// <value><c>true</c> if in preperation; otherwise, <c>false</c>.</value>
		public bool InPreperation { get { return _inPreparation; } }
		
		private float _totalPrepObjectWeight;
		#endregion

		#region Round Boss
		/// <summary>
		/// Each round can have a boss.
		/// </summary>
		public bool RoundHasBoss;
		
		/// <summary>
		/// The round boss prefab.
		/// </summary>
		public GameObject RoundBossPrefab;

		/// <summary>
		/// Countdown until boss is spawned. Countdown begins at round end.
		/// </summary>
		public float BossCountdown;

		/// <summary>
		/// If true player must kill all of the rounds enemies before the boss countdown begins.
		/// </summary>
		public bool OnlySpawnBossWhenAllEnemiesKilled;
		
		/// <summary>
		/// If true, DestroyCurrentEnemiesRequestEvent is raised when countdown to spawn boss begins. 
		/// This is raised even if #BossCountdown is 0.
		/// </summary>
		public bool DestroyOtherEnemiesWhenBossCountdownBegins = false;
		
		/// <summary>
		/// If true, DestroyCurrentEnemiesRequestEvent is raised when boss is spawned. 
		/// </summary>
		public bool DestroyOtherEnemiesWhenBossSpawns = false;
		
		private bool _bossSpawned = false;
		#endregion
		
		#region Round Time
		private float _roundTime;
		/// <summary>
		/// Returns the round time as float.
		/// </summary>
		/// <value>The round time.</value>
		public float RoundTime { get { return _roundTime; } }
		
		/// <summary>
		/// Gets the round time in seconds.
		/// </summary>
		/// <value>The round time in seconds.</value>
		public int RoundTimeInSeconds { get { return (int)_roundTime; } }
		
		/// <summary>
		/// Gets the round time in minutes.
		/// </summary>
		/// <value>The round time in minutes.</value>
		public float RoundTimeInMinutes { get { return Mathf.Floor (_roundTime / 60f); } }
		
		/// <summary>
		/// Returns a string of format "00:00" representing the round time in minutes and seconds.
		/// </summary>
		/// <value>The round time in minutes seconds as formatted string.</value>
		public string RoundTimeInMinutesSeconds {
			get {
				string minutes = Mathf.Floor (_roundTime / 60).ToString ("00");
				string seconds = Mathf.Floor (_roundTime % 60).ToString ("00");
				
				return string.Format ("{0}:{1}", minutes, seconds);
			}
		}
		
		/// <summary>
		/// Gets the round total time, includes #PreperationTime.
		/// </summary>
		/// <value>The round total time.</value>
		public float RoundTotalTime {
			get {
				float prepTime = (HasPreperationTime) ? PreperationTime : 0f;
				return _roundTime + prepTime;
			}
		}
		#endregion
		
		#region Round Progress
		public enum ProgressType
		{
			TimeUp,
			WhenTriggered,
			WaitForTrigger,
			EnemiesKilled
		}
		/// <summary>
		/// Defines how the round progresses to the next round. \n
		/// \link RoundManager::RoundProgressType TimeUp \endlink: round will end when the combined checkpoint time is up. \n
		/// \link RoundManager::RoundProgressType WhenTriggered \endlink: round will end immediately when RoundManager#TriggerRoundEnd is called. \n
		/// \link RoundManager::RoundProgressType WaitForTrigger \endlink: round will end when RoundManager#TriggerRoundEnd is called and the combined checkpoint time is up.
		/// \link RoundManager::RoundProgressType EnemiesKilled \endlink: round will end when all currently spawned enemies are killed.
		/// </summary>
		public ProgressType RoundProgressType = ProgressType.TimeUp;

		private bool _roundEndTriggered = false;
		
		private bool _timeUp = false;
		public bool RoundOver { 
			get { 
			
				switch (RoundProgressType) {
				case ProgressType.TimeUp:
					return _timeUp;
				case ProgressType.WhenTriggered:
					return _roundEndTriggered;
				case ProgressType.WaitForTrigger:
					return _timeUp && _roundEndTriggered;
				case ProgressType.EnemiesKilled:
					var bossPresent = BossPresent ();
					var bossKilled = (bossPresent && _timeUp) || (!bossPresent);
					return _remainingEnemies == 0 && !_inPreparation && _currentCheckpoint >= Checkpoints.Length && bossKilled;
				}
			
				return false;
			} 
		}
		#endregion

		private RoundManager _manager;
		public RoundManager Manager { get { return _manager; } }

		private int _remainingEnemies;
		/// <summary>
		/// Gets the number of spawned enemies.
		/// </summary>
		/// <value>The remaining spawned enemies.</value>
		public int RemainingEnemies { get { return _remainingEnemies; } }

		private bool _bossQueued = false;

		/// <summary>
		/// Invoked by RoundManager. Not required to be called manually.
		/// Called at the beginning of the round. 
		/// Raises \ref RoundManager.Events.DestroyCurrentEnemiesRequestEvent "DestroyCurrentEnemiesRequestEvent"
		/// if #DestroyPreviousRoundEnemiesOnRoundStart is true.
		/// Raises \ref RoundManager.Events.RoundStartEvent "RoundStartEvent".
		/// </summary>
		/// <param name="manager">The rounds owner.</param>
		public void Enter (RoundManager manager)
		{
			_inPreparation = true;
			_timeUp = false;
			_bossSpawned = false;
			_bossQueued = false;
			_manager = manager;

			if (DestroyPreviousRoundEnemiesOnRoundStart) {
				RoundEvents.Instance.Raise (new DestroyCurrentEnemiesRequestEvent (this));
			}

			IncreaseCheckpoint ();

			foreach (var c in Checkpoints) {
				_roundTime += c.CheckpointTime;
			}
			
			RoundEvents.Instance.Raise (new RoundStartEvent (this));
		}

		/// <summary>
		/// Invoked by RoundManager. Not required to be called manually.
		/// Called at the beginning of the round preperation stage. 
		/// If the round has a preperation stage then \ref RoundManager.Events.PreperationStartEvent "PreperationStartEvent". is raised
		/// and preperation objects spawned.
		/// </summary>
		public void OnPreperationStart ()
		{		
			if (!HasPreperationTime)
				return;
				
			RoundEvents.Instance.Raise (new PreperationStartEvent (this));

			if (SpawnObjectsDuringPreperaionTime) {
				foreach (var obj in PreperationTimeObjects) {
					_totalPrepObjectWeight += obj.Weight;
				}
				
				SpawnPreperationObjects ();
			}

			_manager.LogRoundMessage ("Preperation Stage Begun");
		}

		/// <summary>
		/// Invoked by RoundManager. Not required to be called manually.
		/// Called at the end of the rounds preperation stage. 
		/// If the round has a preperation stage then \ref RoundManager.Events.PreperationEndEvent "PreperationEndEvent" is raised.
		/// </summary>
		public void OnPreparationOver ()
		{
			_inPreparation = false;
			
			if (!HasPreperationTime)
				return;

			RoundEvents.Instance.Raise (new PreperationEndEvent (this));


			_manager.LogRoundMessage ("Preperation Stage over");
		}

		/// <summary>
		/// Invoked by RoundManager each time step that round is active. Not required to be called manually.
		/// Calls RoundCheckpoint#Execute for current checkpoint. Increments checkpoint 
		/// when current checkpoints time reaches zero.
		/// </summary>
		public void Execute ()
		{
			if (RoundOver || _timeUp || _bossSpawned || _inPreparation) {
				return;
			}

			if (_bossQueued) {
				if (ShouldSpawnBossNow ()) {
					StartBossSpawn ();
				} 
				return;
			}

			_roundTime -= Time.deltaTime;
			
			if (_roundTime < 0) {
				_roundTime = 0f;
			}
			
			Checkpoints [_currentCheckpoint].CheckpointTime -= Time.deltaTime;

			if (Checkpoints [_currentCheckpoint].CheckpointTime <= 0) {
				Checkpoints [_currentCheckpoint].CheckpointTime = 0;
				_manager.LogRoundMessage ("Starting new checkpoint");
				IncreaseCheckpoint ();
				return;
			}

			Checkpoints [_currentCheckpoint].Execute ();
		}
		
		/// <summary>
		/// Invoked by Round Manager. Not required to be called manually.
		/// Raises \ref RoundManager.Events.RoundEndEvent "RoundEndEvent" and if #DestroyEnemiesOnRoundEnd is true, 
		/// raises \ref RoundManager.Events.DestroyCurrentEnemiesRequestEvent "DestroyCurrentEnemiesRequestEvent".
		/// </summary>
		public void Exit ()
		{
			RoundEvents.Instance.Raise (new RoundEndEvent (this, !_manager.LastRound ()));
			
			if (DestroyEnemiesOnRoundEnd) {
				RoundEvents.Instance.Raise (new DestroyCurrentEnemiesRequestEvent (this));
			}
		}

		/// <summary>
		/// Triggers the round to end immediately if the #RoundProgressType is \link RoundManager::RoundProgressType WhenTriggered \endlink or triggers
		/// the round to end when the round time has finished and the #RoundProgressType is \link RoundManager::RoundProgressType WaitForTrigger \endlink. 
		/// </summary>
		public void TriggerRoundEnd ()
		{
			_roundEndTriggered = true;
		}

		/// <summary>
		/// Invoked from RoundBoss when the boss is killed. Raises \ref RoundManager.Events.BossKilledEvent "BossKilledEvent".
		/// </summary>
		public void BossKilled ()
		{
			TimeUp ();
			
			RoundEvents.Instance.Raise (new BossKilledEvent (this));
			
			_manager.LogRoundMessage ("all checkpoints finished");
		}
		
		/// <summary>
		/// Called if #RoundHasBoss. If #DestroyOtherEnemiesWhenBossCountdownBegins is true then raises
		/// \ref RoundManager.Events.DestroyCurrentEnemiesRequestEvent "DestroyCurrentEnemiesRequestEvent".
		/// If #BossCountdown is greater than zero then a
		/// \ref RoundManager.Events.BossCountdownBegunEvent "BossCountdownBegunEvent" event is raised.
		/// \ref RoundManager.Events.BossSpawnRequestEvent "BossSpawnRequestEvent" is raised once the countdown reaches zero.
		/// If #DestroyOtherEnemiesWhenBossSpawns is truen then a 
		/// \ref RoundManager.Events.DestroyCurrentEnemiesRequestEvent "DestroyCurrentEnemiesRequestEvent" is raised.
		/// </summary>
		/// <returns>The boss.</returns>
		public IEnumerator SpawnBoss ()
		{
			if (DestroyOtherEnemiesWhenBossCountdownBegins) {
				RoundEvents.Instance.Raise (new DestroyCurrentEnemiesRequestEvent (this));
			}
			
			
			if (BossCountdown > 0) {
				RoundEvents.Instance.Raise (new BossCountdownBegunEvent (this));
			}
			
			while (BossCountdown > 0) {
				BossCountdown--;
				
				yield return new WaitForSeconds (1f);
			}
			
			RoundEvents.Instance.Raise (new BossSpawnRequestEvent (this, RoundBossPrefab));
			
			
			if (DestroyOtherEnemiesWhenBossSpawns) {
				RoundEvents.Instance.Raise (new DestroyCurrentEnemiesRequestEvent (this));
			}
		}

		/// <summary>
		/// Registers an enemy spawned. Used when #OnlySpawnBossWhenAllEnemiesKilled is true.
		/// Called by RoundEnemy.
		/// </summary>
		public void RegisterEnemySpawned ()
		{
			_remainingEnemies++;

		}

		/// <summary>
		/// Registers an enemy killed. Used when #OnlySpawnBossWhenAllEnemiesKilled is true.
		/// Called by RoundEnemy.
		/// </summary>
		public void RegisterEnemyKilled ()
		{
			_remainingEnemies--;
		}
		
		private void TimeUp ()
		{
			_timeUp = true;
			
			_manager.LogRoundMessage ("all checkpoints finished");
		}

		private void QueueBossSpawn ()
		{
			_bossQueued = true;
		}

		private void StartBossSpawn ()
		{
			_manager.LogRoundMessage ("Spawning boss");
			_manager.CoroutineCallback (SpawnBoss);
			_bossSpawned = true;
		}

		private void IncreaseCheckpoint ()
		{
			_currentCheckpoint ++;
			
			RoundEvents.Instance.Raise (new CheckpointEndEvent (this, _currentCheckpoint >= Checkpoints.Length));
			
			if (_currentCheckpoint >= Checkpoints.Length) {
				if (BossPresent () && ShouldSpawnBossNow ()) {
					StartBossSpawn ();
				} else {
					if (BossPresent ()) {
						QueueBossSpawn ();
					} else {
						TimeUp ();
					}
				}
			} else {
				RoundEvents.Instance.Raise (new CheckpointStartEvent (this));
				
				Checkpoints [_currentCheckpoint].Enter ();
			}
		}

		private void SpawnPreperationObjects ()
		{
			if (PreperationTimeObjects == null || PreperationTimeObjects.Length == 0)
				return;
		
			for (int i = 0; i < NumberOfObjectsToSpawn; i++) {
				RoundEvents.Instance.Raise (new ObjectSpawnRequestEvent (this, PreperationTimeObjects [GetIndex ()].Prefab));
			}

			SpawnObjectsDuringPreperaionTime = false;
		}

		private int GetIndex ()
		{
			if (PreperationTimeObjects.Length == 1) {
				return 0;
			}
			
			var randomIndex = -1;
			var random = Random.value * _totalPrepObjectWeight;
			
			for (int i = 0; i < PreperationTimeObjects.Length; ++i) {
				random -= PreperationTimeObjects [i].Weight;
				
				if (random <= 0f) {
					randomIndex = i;
					break;
				}
			}
			
			return randomIndex;
		}

		private bool BossPresent ()
		{
			return RoundHasBoss && RoundBossPrefab != null; 
		}
	
		private bool ShouldSpawnBossNow ()
		{
			return OnlySpawnBossWhenAllEnemiesKilled ? _remainingEnemies <= 0 : true;
		}


	}
}