using UnityEngine;
using System.Collections;

namespace RoundManager.Events
{
	/// <summary>
	/// Raised to signify that all currently spawned enemies should be destroyed.
	/// See DestroyCurrentEnemiesRequest template and EnemySpawner for a concrete example of how
	/// to react to this event.
	/// </summary>
	public class DestroyCurrentEnemiesRequestEvent : RoundEvent
	{
		private Round _currentRound;
		/// <summary>
		/// Gets the current round that raised this event.
		/// </summary>
		/// <value>The current round.</value>
		public Round CurrentRound { get { return _currentRound; } }

		public DestroyCurrentEnemiesRequestEvent (Round currentRound)
		{
			_currentRound = currentRound;
		}
	}
}
