using UnityEngine;
using System.Collections;

namespace RoundManager.Events
{
	/// <summary>
	/// Raised when the an enemy should be spawned. See EnemySpawnRequest template and
	/// EnemySpawner for a concrete example of how to react to the event.
	/// </summary>
	public class EnemySpawnRequestEvent : ObjectSpawnRequestEvent
	{
		public EnemySpawnRequestEvent (Round currentRound, GameObject enemyPrefab) : base (currentRound, enemyPrefab)
		{
		}
	}
}
