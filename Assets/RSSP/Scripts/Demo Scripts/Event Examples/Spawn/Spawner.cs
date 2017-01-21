using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Base Class for ObjectSpawner and EnemySpawner. Provides functionality to instantiate object.
	/// </summary>
	public abstract class Spawner : MonoBehaviour
	{
		public Environment Level;

		protected GameObject SpawnObject (GameObject prefab, List<Transform> spawnTiles)
		{
			if (prefab == null)
				return null;
			
			RoundManager.Instance.LogRoundMessage ("Spawning: " + prefab.name);
			
			var spawnTile = spawnTiles [Random.Range (0, spawnTiles.Count)];
			
			var pos = spawnTile.position;
			
			var obj = (GameObject)Instantiate (prefab, pos, Quaternion.identity);
			obj.transform.SetParent (transform);
			
			return obj;
			
		}
		
	}
}
