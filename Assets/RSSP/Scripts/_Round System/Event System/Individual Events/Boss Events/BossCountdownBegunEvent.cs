using UnityEngine;
using System.Collections;

namespace RoundManager.Events
{
	/// <summary>
	/// Raised when the boss countdown begins. See BossCountdownBegun template and
	/// BossCountdownText for a concrete example of how to react to the event.
	/// </summary>
	public class BossCountdownBegunEvent : RoundEvent
	{
		private Round _currentRound;
		
		/// <summary>
		/// Gets the current round that raised this event.
		/// </summary>
		/// <value>The current round.</value>
		public Round CurrentRound { get { return _currentRound; } }

		public BossCountdownBegunEvent (Round currentRound)
		{
			_currentRound = currentRound;
		}
		
	}
}
