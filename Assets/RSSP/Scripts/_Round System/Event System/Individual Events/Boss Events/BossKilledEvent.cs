using UnityEngine;
using System.Collections;

namespace RoundManager.Events
{
	/// <summary>
	/// Raised when the round boss is killed. See BossKilled template for a template of how to react to the event.
	/// </summary>
	public class BossKilledEvent : RoundEvent
	{
		private Round _currentRound;
		
		/// <summary>
		/// Gets the current round that raised this event.
		/// </summary>
		/// <value>The current round.</value>
		public Round CurrentRound { get { return _currentRound; } }
		
		public BossKilledEvent (Round currentRound)
		{
			_currentRound = currentRound;
		}

	}
}
