using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Responsible for raising \ref RoundManager.Events.EnemySpawnRequestEvent "EnemySpawnRequestEvent".
	/// </summary>
	[System.Serializable]
	public class RoundCheckpoint
	{
		#region Checkpoint Time
		/// <summary>
		/// Total checkpoint time.
		/// </summary>
		public float CheckpointTime = 10;
		
		/// <summary>
		/// Gets the checkpoint time in seconds.
		/// </summary>
		/// <value>The checkpoint time in seconds.</value>
		public int CheckpointTimeInSeconds { get { return (int)CheckpointTime; } }
		
		/// <summary>
		/// Gets the checkpoint time in minutes.
		/// </summary>
		/// <value>The checkpoint time in minutes.</value>
		public float CheckpointTimeInMinutes { get { return Mathf.Floor (CheckpointTime / 60f); } }
		
		/// <summary>
		/// Returns a string of format "00:00" representing the checkpoint time in minutes and seconds.
		/// </summary>
		/// <value>The checkpoint time in minutes seconds as formatted string.</value>
		public string CheckpointTimeInMinutesSeconds {
			get {
				string minutes = Mathf.Floor (CheckpointTime / 60).ToString ("00");
				string seconds = Mathf.Floor (CheckpointTime % 60).ToString ("00");
				
				return string.Format ("{0}:{1}", minutes, seconds);
			}
		}
		#endregion
		
		#region Spawn Variables
		/// <summary>
		/// A number between 0 and 1. 
		/// Every time an enemy is to be spawned, a random number between 0 and 1 is generated. If this random number is less than
		/// or equal to this then the enemy is spawned.
		/// Higher number results in a greater chance to spawn an enemy and 1 results in a spawned enemy every time.
		/// </summary>
		public float EnemySpawnChance = 1;
		
		/// <summary>
		/// The time between enemy spawns.
		/// </summary>
		public float TimeBetweenEnemySpawns = 0.5f;
		
		/// <summary>
		/// The number of enemies spawned during this checkpoint will be limited if this is true.
		/// </summary>
		public bool LimitEnemyCount;
		
		/// <summary>
		/// If #LimitEnemyCount is true, this number will cap the number of enemies spawned.
		/// </summary>
		public int MaxEnemies;
		#endregion
		
		/// <summary>
		/// The enemies owned by this checkpoint.
		/// </summary>
		public RoundObjectSpawner[] RoundEnemies;

		private float _totalWeight;
		private float _currentTime;
		private int _currentEnemyCount;
		
		/// <summary>
		/// Invoked by Round. Not required to be called manually.
		/// Called at the beginning of each checkpoint. 
		/// </summary>
		public void Enter ()
		{
			_currentTime = 0f;
			_currentEnemyCount = 0;
			
			InitialiseWeights ();
		}
		
		/// <summary>
		/// Invoked by Round each time step that checkpoint is active. Not required to be called manually.
		/// Raises \ref RoundManager.Events.EnemySpawnRequestEvent "EnemySpawnRequestEvent" if:
		/// \n 
		/// - #RoundEnemies count greater than 0.
		/// - The current time between enemy spawns is >= #TimeBetweenEnemySpawns.
		/// - A random value between 0 and 1 is <= #EnemySpawnChance.
		/// </summary>
		public void Execute ()
		{
			_currentTime += Time.deltaTime;

			if (OkToSpawn ()) {
				_currentTime = 0f;
				SpawnEnemy ();
			}
		}
		
		private void InitialiseWeights ()
		{
			foreach (var enemy in RoundEnemies) {
				_totalWeight += enemy.Weight;
			}
		}
		
		private bool OkToSpawn ()
		{
			return EntitiesReadyToSpawn () && _currentTime >= TimeBetweenEnemySpawns && Random.value <= EnemySpawnChance;
		}
		
		private void SpawnEnemy ()
		{
			if (LimitEnemyCount && _currentEnemyCount >= MaxEnemies)
				return;
			
			var index = GetIndex ();
			
			RoundEvents.Instance.Raise (new EnemySpawnRequestEvent (RoundManager.Instance.CurrentRound, RoundEnemies [index].Prefab));
			
			_currentEnemyCount++;
		}

		private bool EntitiesReadyToSpawn ()
		{
			return RoundEnemies != null && RoundEnemies.Length > 0;
		}

		private int GetIndex ()
		{
			if (RoundEnemies.Length == 1) {
				return 0;
			}

			var randomIndex = -1;
			var random = Random.value * _totalWeight;
			
			for (int i = 0; i < RoundEnemies.Length; ++i) {
				random -= RoundEnemies [i].Weight;
				
				if (random <= 0f) {
					randomIndex = i;
					break;
				}
			}

			return randomIndex;
		}
		
	}
}
