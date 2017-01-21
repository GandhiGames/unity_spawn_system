using UnityEngine;
using System.Collections;

namespace RoundManager.Events
{
	/// <summary>
	/// Raised to signify that all rounds have finished.
	/// See FinishedRounds for a template of how to react to this event and RoundsFinishedText
	/// for a concrete example.
	/// </summary>
	public class FinishedRoundsEvent : RoundEvent
	{
		private Round _lastRound;
		/// <summary>
		/// Returns the last round.
		/// </summary>
		/// <value>The last round.</value>
		public Round LastRound { get { return _lastRound; } }
		
		public FinishedRoundsEvent (Round currentRound)
		{
			_lastRound = currentRound;
		}
	}
}
