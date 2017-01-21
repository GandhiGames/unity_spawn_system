using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Example class reacting to round event \ref RoundManager.Events.ObjectSpawnRequestEvent "ObjectSpawnRequestEvent". \n
	/// Spawns preperation object when \ref RoundManager.Events.ObjectSpawnRequestEvent "ObjectSpawnRequestEvent" is raised. \n
	/// </summary>
	public class ObjectSpawner : Spawner
	{
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<ObjectSpawnRequestEvent> (OnSpawnObject);
		}
	
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<ObjectSpawnRequestEvent> (OnSpawnObject);
		}
	
		private void OnSpawnObject (ObjectSpawnRequestEvent e)
		{
			SpawnObject (e.ObjectPrefab, Level.ObjectSpawnTiles);
		}
	
	
	}
}
