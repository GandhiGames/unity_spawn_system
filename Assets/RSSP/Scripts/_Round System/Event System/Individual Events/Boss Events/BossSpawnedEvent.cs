using UnityEngine;
using System.Collections;

namespace RoundManager.Events
{
	/// <summary>
	/// Raised when a boss is spawned.
	/// See BossSpawned for a template of how to react to this event.
	/// The event is currently raised in EnemySpawner. If you want to react to this event, and you
	/// are using a custom boss spawner then ensure that the event is raised when a boss is spawned.
	/// </summary>
	public class BossSpawnedEvent : RoundEvent
	{
		private GameObject _boss;
		/// <summary>
		/// Returns the instantiated boss.
		/// </summary>
		/// <value>The boss.</value>
		public GameObject Boss { get { return _boss; } }
		
		private Round _currentRound;
		public Round CurrentRound { get { return _currentRound; } }
		
		public BossSpawnedEvent (Round currentRound, GameObject boss)
		{
			_currentRound = currentRound;
			_boss = boss;
		}

	}
}
