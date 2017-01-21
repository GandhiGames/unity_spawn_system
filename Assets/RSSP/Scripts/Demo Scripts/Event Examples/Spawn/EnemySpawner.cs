using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Example class reacting to spawn request events. \n
	/// Spawns enemy when \ref RoundManager.Events.EnemySpawnRequestEvent "EnemySpawnRequestEvent" is raised. \n
	/// Spawns boss when \ref RoundManager.Events.BossSpawnRequestEvent "BossSpawnRequestEvent" is raised. \n
	/// Destroys all spawned enemies when \ref RoundManager.Events.DestroyCurrentEnemiesRequestEvent "DestroyCurrentEnemiesRequestEvent" is raised.
	/// </summary>
	public class EnemySpawner : Spawner
	{
		private List<EnemyHealth> _enemies = new List<EnemyHealth> ();
		
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<BossSpawnRequestEvent> (OnSpawnBoss);
			RoundEvents.Instance.AddListener<EnemySpawnRequestEvent> (OnSpawnEnemy);
			RoundEvents.Instance.AddListener<DestroyCurrentEnemiesRequestEvent> (OnDestroyObjects);
		}
	
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<BossSpawnRequestEvent> (OnSpawnBoss);
			RoundEvents.Instance.RemoveListener<EnemySpawnRequestEvent> (OnSpawnEnemy);
			RoundEvents.Instance.RemoveListener<DestroyCurrentEnemiesRequestEvent> (OnDestroyObjects);
		}
		
		private void OnSpawnEnemy (EnemySpawnRequestEvent e)
		{
			var enemy = SpawnObject (e.ObjectPrefab, Level.EnemySpawnTiles);
			_enemies.Add (enemy.GetComponent<EnemyHealth> ());
		}
		
		private void OnSpawnBoss (BossSpawnRequestEvent e)
		{
			var boss = SpawnObject (e.ObjectPrefab, Level.EnemySpawnTiles);
			RoundEvents.Instance.Raise (new BossSpawnedEvent (RoundManager.Instance.CurrentRound, boss));
		}
		
		private void OnDestroyObjects (DestroyCurrentEnemiesRequestEvent e)
		{
			foreach (var obj in _enemies) {
				if (obj)
					obj.OnDead ();
			}
			
			_enemies.Clear ();
		}
	}
}
