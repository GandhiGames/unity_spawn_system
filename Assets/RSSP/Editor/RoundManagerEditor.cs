using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace RoundManager
{
	[CustomEditor (typeof(RoundManager)), CanEditMultipleObjects]
	public class RoundManagerEditor : Editor
	{

		private SerializedProperty _rounds;
		private SerializedProperty _debugMessages;

		private bool _showRoundSettings = true;

		private List<bool> _showGeneralRoundSettings;

		private List<bool> _showRoundCheckpointSettings;

		private List<bool> _showRounds;

		private List<RoundCheckpintVisibilitySettings> _showIndividualCheckpoints;


		private static GUIContent
			insertContent = new GUIContent ("+", "duplicate this entry"),
			deleteContent = new GUIContent ("-", "delete this entry");

		private static GUILayoutOption
			buttonWidth = GUILayout.MaxWidth (20f);

		private static GUIStyle helpStyle;

		void Awake ()
		{
			_rounds = serializedObject.FindProperty ("Rounds");
		}


		#region General Round Settings
		private void ShowGeneralRoundSettings (SerializedProperty roundProp)
		{
			EditorGUILayout.BeginHorizontal ();
			GUILayout.Label ("Destroy Previous Round Enemies On Round Start: ", GUILayout.MinWidth (200));
			EditorGUILayout.PropertyField (roundProp.FindPropertyRelative ("DestroyPreviousRoundEnemiesOnRoundStart"), GUIContent.none);
			EditorGUILayout.EndHorizontal ();
			
			EditorGUILayout.BeginHorizontal ();
			GUILayout.Label ("Destroy Enemies On Round End: ", GUILayout.MinWidth (200));
			EditorGUILayout.PropertyField (roundProp.FindPropertyRelative ("DestroyEnemiesOnRoundEnd"), GUIContent.none);
			EditorGUILayout.EndHorizontal ();

			
			GUILayout.Label ("");
			EditorGUILayout.LabelField ("Round Progress Type", EditorStyles.boldLabel);
			AddHelpLabel ("The Round Progress Type defines how the round will end.\n" + 
				"\nTime Up: Round will end when each checkpoints time is up and boss (if round has boss) is killed." +
				"\n\nWhen Triggered: Round will end when 'RoundManager.Instance.CurrentRound.TriggerRoundEnd ()' is called, regardless of the round time left." +
				"\n\nWait For Trigger: Round will end when time is up and 'RoundManager.Instance.CurrentRound.TriggerRoundEnd ()' is called." +
				"\n\nEnemiesKilled: Round will end when all spawned enemies are killed.");
			EditorGUILayout.PropertyField (roundProp.FindPropertyRelative ("RoundProgressType"));
			    
			GUILayout.Label ("");
			EditorGUILayout.LabelField ("Preperation", EditorStyles.boldLabel);
			
			AddHelpLabel ("The time before a round starts. Use this time to allow the player to prepare for the next round." +
				" No enemies will spawn during this time however 'Preperation objects' can be spawned. These could be items that aid the player i.e. weapons, health etc.");
			
			EditorGUILayout.PropertyField (roundProp.FindPropertyRelative ("HasPreperationTime"));
			if (roundProp.FindPropertyRelative ("HasPreperationTime").boolValue) {
				EditorGUI.indentLevel++;
				EditorGUILayout.PropertyField (roundProp.FindPropertyRelative ("PreperationTime"));
				
				EditorGUILayout.PropertyField (roundProp.FindPropertyRelative ("SpawnObjectsDuringPreperaionTime"));
				
				if (roundProp.FindPropertyRelative ("SpawnObjectsDuringPreperaionTime").boolValue) {
					EditorGUI.indentLevel++;
					EditorGUILayout.PropertyField (roundProp.FindPropertyRelative ("NumberOfObjectsToSpawn"));
					
					var prepTimeObjects = roundProp.FindPropertyRelative ("PreperationTimeObjects");
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField ("Preperation Objects");
					if (GUILayout.Button (insertContent, EditorStyles.miniButtonLeft, buttonWidth)) {
						prepTimeObjects.InsertArrayElementAtIndex (0);
					}
					if (GUILayout.Button (deleteContent, EditorStyles.miniButtonRight, buttonWidth) && prepTimeObjects.arraySize > 0) {
						prepTimeObjects.DeleteArrayElementAtIndex (0);
					}
					EditorGUILayout.EndHorizontal ();
					
					EditorGUI.indentLevel++;
					for (int j = 0; j < prepTimeObjects.arraySize; j++) {					
						var prepProperty = prepTimeObjects.GetArrayElementAtIndex (j);
						
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.PropertyField (prepProperty.FindPropertyRelative ("Prefab"), GUIContent.none);
						EditorGUILayout.PropertyField (prepProperty.FindPropertyRelative ("Weight"));
						
						if (GUILayout.Button (insertContent, EditorStyles.miniButtonLeft, buttonWidth)) {
							prepTimeObjects.InsertArrayElementAtIndex (0);
						}
						if (GUILayout.Button (deleteContent, EditorStyles.miniButtonRight, buttonWidth) && prepTimeObjects.arraySize > 0) {
							prepTimeObjects.DeleteArrayElementAtIndex (0);
						}
						EditorGUILayout.EndHorizontal ();
					}
					EditorGUI.indentLevel--;
					EditorGUI.indentLevel--;
				}
				
				EditorGUI.indentLevel--;
			}
			
			GUILayout.Label ("");
			EditorGUILayout.LabelField ("Round Boss", EditorStyles.boldLabel);
			
			AddHelpLabel ("Each round can have it's own boss. Here you can choose the boss for this round. The boss countdown begins at the end of the round and when the" +
				" countdown reaches zero, the boss will be spawned. You can also choose whether to destroy enemies when the boss countdown begins (giving time for the player to prepare)" +
				" or destroy the round enemies when the boss spawns, giving the player time to fight them while the boss countdown is in progress.");
			
			EditorGUILayout.PropertyField (roundProp.FindPropertyRelative ("RoundHasBoss"));
			if (roundProp.FindPropertyRelative ("RoundHasBoss").boolValue) {
				EditorGUI.indentLevel++;
				EditorGUILayout.PropertyField (roundProp.FindPropertyRelative ("RoundBossPrefab"));
				EditorGUILayout.PropertyField (roundProp.FindPropertyRelative ("BossCountdown"));

				EditorGUILayout.PropertyField (roundProp.FindPropertyRelative ("OnlySpawnBossWhenAllEnemiesKilled"));

				if (!roundProp.FindPropertyRelative ("OnlySpawnBossWhenAllEnemiesKilled").boolValue) {
					EditorGUI.indentLevel++;
					EditorGUILayout.PropertyField (roundProp.FindPropertyRelative ("DestroyOtherEnemiesWhenBossCountdownBegins"));
					EditorGUILayout.PropertyField (roundProp.FindPropertyRelative ("DestroyOtherEnemiesWhenBossSpawns"));
					EditorGUI.indentLevel--;
				}
				EditorGUI.indentLevel--;
			}
		}
		#endregion

		#region Checkpoints
		private void ShowIndividualCheckpoint (SerializedProperty checkProp, int checkpointIndex, int roundIndex)
		{
		
		
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Duplicate or Remove Checkpoint:");
			if (GUILayout.Button (insertContent, EditorStyles.miniButtonLeft, buttonWidth)) {
				checkProp.InsertArrayElementAtIndex (checkpointIndex);
				AddCheckpointVisibilitySetting (roundIndex, checkpointIndex);
			}
			if (GUILayout.Button (deleteContent, EditorStyles.miniButtonRight, buttonWidth)) {
				checkProp.DeleteArrayElementAtIndex (checkpointIndex);
				RemoveCheckpointVisibilitySetting (roundIndex, checkpointIndex);
				return;
			}
			EditorGUILayout.EndHorizontal ();

			
			EditorGUI.indentLevel++;
			
			var checkpoint = checkProp.GetArrayElementAtIndex (checkpointIndex);
			
			EditorGUILayout.PropertyField (checkpoint.FindPropertyRelative ("CheckpointTime"));
			EditorGUILayout.Slider (checkpoint.FindPropertyRelative ("EnemySpawnChance"), 0f, 1f);
			EditorGUILayout.PropertyField (checkpoint.FindPropertyRelative ("TimeBetweenEnemySpawns"));
			
			EditorGUILayout.PropertyField (checkpoint.FindPropertyRelative ("LimitEnemyCount"));
			if (checkpoint.FindPropertyRelative ("LimitEnemyCount").boolValue) {
				EditorGUI.indentLevel++;
				EditorGUILayout.PropertyField (checkpoint.FindPropertyRelative ("MaxEnemies"));
				EditorGUI.indentLevel--;
			}
			
			GUILayout.Label (string.Empty);
			
			var checkEnemiesProp = checkpoint.FindPropertyRelative ("RoundEnemies");
			EditorGUILayout.BeginHorizontal ();
			GUILayout.Label ("Checkpoint Enemies");
			if (GUILayout.Button (insertContent, EditorStyles.miniButtonLeft, buttonWidth)) {
				checkEnemiesProp.InsertArrayElementAtIndex (0);
			}
			if (GUILayout.Button (deleteContent, EditorStyles.miniButtonRight, buttonWidth) && checkEnemiesProp.arraySize > 0) {
				checkEnemiesProp.DeleteArrayElementAtIndex (0);
			}
			EditorGUILayout.EndHorizontal ();
			
			AddHelpLabel ("Select the enemies to spawn during this checkpoint and their respective weight. The higher the " +
				"weight value, the greater the chance of that enemy spawning");
			
			for (int l = 0; l < checkEnemiesProp.arraySize; l++) {
				var enemy = checkEnemiesProp.GetArrayElementAtIndex (l);
				
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (enemy.FindPropertyRelative ("Prefab"), GUIContent.none);
				EditorGUILayout.PropertyField (enemy.FindPropertyRelative ("Weight"));
				
				if (GUILayout.Button (insertContent, EditorStyles.miniButtonLeft, buttonWidth)) {
					checkEnemiesProp.InsertArrayElementAtIndex (l);
				}
				if (GUILayout.Button (deleteContent, EditorStyles.miniButtonRight, buttonWidth)) {
					checkEnemiesProp.DeleteArrayElementAtIndex (l);
				}
				
				EditorGUILayout.EndHorizontal ();
			}
			
			EditorGUI.indentLevel--;
		}

		private void AddCheckpointVisibilitySetting (int roundIndex, int checkpointIndex)
		{
			_showIndividualCheckpoints [roundIndex].RoundCheckpointVisibility.Insert (checkpointIndex, true);
		}

		private void RemoveCheckpointVisibilitySetting (int roundIndex, int checkpointIndex)
		{
			_showIndividualCheckpoints [roundIndex].RoundCheckpointVisibility.RemoveAt (checkpointIndex);
		}

		private void ShowCheckpointSettings (SerializedProperty checkProp, int roundIndex)
		{

			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Checkpoints", EditorStyles.boldLabel);
			if (GUILayout.Button (insertContent, EditorStyles.miniButtonLeft, buttonWidth)) {
				checkProp.InsertArrayElementAtIndex (0);
				AddCheckpointVisibilitySetting (roundIndex, 0);
			}

			EditorGUILayout.EndHorizontal ();

			string checkpointHelpLabel = "A round consists of a number of checkpoints. You can set different enemies to spawn duing each checkpoint." +
				" This way you can have harder enemies spawn towards the end of a round.\n\n" +
				"Checkpoint Time: how long the checkpoint is in progress. Adds to the overal time of the round." +
				"\nEnemy Spawn Chance: the chance that an enemy will spawn once Time Between Enemy Spawn has been reached (0 = never, 1 = always). Lower numbers results in less enemy spawns." +
				"\nTime Between Enemy Spawns: lower numbers result in a quicker enemy spawn time. For example, if you set this to 1 then" + 
				"an attempt to spawn an enemy will occur every 1 second (depending on enemy spawn chance)." +
				"\nLimit Enemy Count: limits the maximum number of enemies that can be spawned during a checkpoint.";
			
			AddHelpLabel (checkpointHelpLabel);
			
		
			
			for (int k = 0; k < checkProp.arraySize; k++) {
				EditorGUILayout.BeginVertical ("Box");
				_showIndividualCheckpoints [roundIndex].RoundCheckpointVisibility [k] = EditorGUILayout.Foldout (_showIndividualCheckpoints [roundIndex].RoundCheckpointVisibility [k], 
				                                                                            	string.Format ("Checkpoint {0}", (k + 1)));
				if (_showIndividualCheckpoints [roundIndex].RoundCheckpointVisibility [k]) {
					ShowIndividualCheckpoint (checkProp, k, roundIndex);
				}
				EditorGUILayout.EndVertical ();
				
			}
		}
		#endregion

		private void ShowInidividualRoundSettings (SerializedProperty roundProp, int i, RoundManager roundManager)
		{


			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Duplicate Or Remove Round:");


			if (GUILayout.Button (insertContent, EditorStyles.miniButtonLeft, buttonWidth)) {
				_rounds.InsertArrayElementAtIndex (i);
				_showGeneralRoundSettings.Insert (i, true);
				_showRoundCheckpointSettings.Insert (i, true);
				_showRounds.Insert (i, true);
			}
			if (GUILayout.Button (deleteContent, EditorStyles.miniButtonRight, buttonWidth) && _rounds.arraySize > 0) {
				_rounds.DeleteArrayElementAtIndex (i);
				_showGeneralRoundSettings.RemoveAt (i);
				_showRoundCheckpointSettings.RemoveAt (i);
				_showRounds.RemoveAt (i);
				return;
			}

			EditorGUILayout.EndHorizontal ();

			bool destroyEnemiesAtStart = false;
			bool destroyEnemiesAtRoundEnd = false;
			float currentRoundTime = 0f;
			float preperationTime = 0f;
			bool roundHasBoss = false;
			int numOfCheckpoints = 0;

			if (roundManager.Rounds.Length > i) {
			

				foreach (var checkpoint in roundManager.Rounds[i].Checkpoints) {
					currentRoundTime += checkpoint.CheckpointTime;
				}

				destroyEnemiesAtStart = roundManager.Rounds [i].DestroyPreviousRoundEnemiesOnRoundStart;
				destroyEnemiesAtRoundEnd = roundManager.Rounds [i].DestroyEnemiesOnRoundEnd;
				preperationTime = (roundManager.Rounds [i].HasPreperationTime) ? roundManager.Rounds [i].PreperationTime : 0f;
				roundHasBoss = roundManager.Rounds [i].RoundHasBoss;
				numOfCheckpoints = roundManager.Rounds [i].Checkpoints.Length;
			}
			
			string roundOverview = "Round Overview:\n" +
				string.Format ("\nDestroy Enemies On Round Start: {0} \nDestroy Enemies On Round End: {1} \nPreperation Time: {2} seconds" +
				" \nCurrent round time: {3} seconds ({4} checkpoint(s))" +
				"\nRound Boss: {5}", destroyEnemiesAtStart, destroyEnemiesAtRoundEnd,
						 preperationTime, currentRoundTime, numOfCheckpoints, roundHasBoss);

			AddHelpLabel (roundOverview);
			
			_showGeneralRoundSettings [i] = EditorGUILayout.Foldout (_showGeneralRoundSettings [i], "Round General Settings");
			if (_showGeneralRoundSettings [i]) {
				ShowGeneralRoundSettings (roundProp);
			}
			
			var checkProp = roundProp.FindPropertyRelative ("Checkpoints");
			
			if (_showIndividualCheckpoints == null) {
				_showIndividualCheckpoints = new List<RoundCheckpintVisibilitySettings> ();
			}

		
			if (_showIndividualCheckpoints.Count <= i) {
				_showIndividualCheckpoints.Insert (i, new RoundCheckpintVisibilitySettings (checkProp.arraySize));
			}
			
			
			_showRoundCheckpointSettings [i] = EditorGUILayout.Foldout (_showRoundCheckpointSettings [i], "Checkpoints");
			if (_showRoundCheckpointSettings [i]) {
				ShowCheckpointSettings (checkProp, i);
			}

		}

		private void SetupVisibility ()
		{
			if (_showGeneralRoundSettings == null) {
				_showGeneralRoundSettings = new List<bool> ();
				
				for (int i = 0; i <_rounds.arraySize; i++) {
					_showGeneralRoundSettings.Add (false);
				}
			}
			
			if (_showRoundCheckpointSettings == null) {
				_showRoundCheckpointSettings = new List<bool> ();
				
				for (int i = 0; i <_rounds.arraySize; i++) {
					_showRoundCheckpointSettings.Add (false);
				}
			}
			
			if (_showRounds == null) {
				_showRounds = new List<bool> ();
				
				for (int i = 0; i <_rounds.arraySize; i++) {
					_showRounds.Add (true);
				}
			}
		}
		
		private void ShowRoundSettings (RoundManager roundManager)
		{
			if (_rounds == null) {
				_rounds = serializedObject.FindProperty ("Rounds");
			}
	
			
			EditorGUI.BeginChangeCheck ();
			
			SetupVisibility ();

			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Rounds", EditorStyles.boldLabel);

			if (GUILayout.Button (insertContent, EditorStyles.miniButtonLeft, buttonWidth)) {
				_rounds.InsertArrayElementAtIndex (0);
				_showGeneralRoundSettings.Insert (0, true);
				_showRoundCheckpointSettings.Insert (0, true);
				_showRounds.Insert (0, true);
			}

			EditorGUILayout.EndHorizontal ();
		
			
			for (int i = 0; i < _rounds.arraySize; i++) {
				var roundProp = _rounds.GetArrayElementAtIndex (i);

				_showRounds [i] = EditorGUILayout.Foldout (_showRounds [i], "Round " + (i + 1));
				if (_showRounds [i]) {
					ShowInidividualRoundSettings (roundProp, i, roundManager);

					GUILayout.Box ("", GUILayout.ExpandWidth (true), GUILayout.Height (1));
				}
				
			}
			
			//EditorGUILayout.PropertyField (_rounds, true);
			if (EditorGUI.EndChangeCheck ()) {
				serializedObject.ApplyModifiedProperties ();
			}
		}
	

		private void ShowRoundManager (RoundManager roundManager)
		{

			EditorGUI.BeginChangeCheck ();
			
			EditorGUILayout.PropertyField (serializedObject.FindProperty ("ShowDebugMessages"));
			
			if (EditorGUI.EndChangeCheck ()) {
				serializedObject.ApplyModifiedProperties ();
			}

			_showRoundSettings = EditorGUILayout.Foldout (_showRoundSettings, "Round Settings");
		

			if (_showRoundSettings) {
			
				string roundHelpText = "Setup and add/remove rounds.\n" 
					+ string.Format ("\nCurrent number of rounds: {0}", roundManager.Rounds.Length);
			
				AddHelpLabel (roundHelpText);
				ShowRoundSettings (roundManager);
			}


		}

		private void AddHelpLabel (string text)
		{
			if (helpStyle == null) {
				helpStyle = new GUIStyle (GUI.skin.box);
				helpStyle.wordWrap = true;
				helpStyle.alignment = TextAnchor.UpperLeft;
			}
			GUILayout.Label (text, helpStyle, GUILayout.ExpandWidth (true));
		}

		public override void OnInspectorGUI ()
		{
			serializedObject.Update ();

			var roundManager = target as RoundManager;

			ShowRoundManager (roundManager);
			
			
			if (GUI.changed) {
				EditorApplication.MarkSceneDirty ();
			}
			
		}
	}

	class RoundCheckpintVisibilitySettings
	{
		public List<bool> RoundCheckpointVisibility;
		
		public RoundCheckpintVisibilitySettings (int count)
		{
			RoundCheckpointVisibility = new List<bool> ();
			
			for (int i = 0; i < count; i++) {
				RoundCheckpointVisibility.Add (false);
			}
			
		}
		
	}
}
