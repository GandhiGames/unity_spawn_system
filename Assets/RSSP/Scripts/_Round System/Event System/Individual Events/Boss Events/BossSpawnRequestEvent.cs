using UnityEngine;
using System.Collections;

namespace RoundManager.Events
{
	/// <summary>
	/// Raised when the a boss should be spawned. See BossSpawnRequest template and
	/// EnemySpawner for a concrete example of how to react to the event.
	/// </summary>
	public class BossSpawnRequestEvent : EnemySpawnRequestEvent
	{
		public BossSpawnRequestEvent (Round currentRound, GameObject enemyPrefab) : base (currentRound, enemyPrefab)
		{
		
		}
	}
}

