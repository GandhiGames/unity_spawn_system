using UnityEngine;
using System.Collections;

namespace RoundManager.Events
{
	/// <summary>
	/// Raised to signify that the last round has started.
	/// See LastRound for a template of how to react to this event.
	/// </summary>
	public class LastRoundEvent : RoundEvent
	{
		private Round _round;
		/// <summary>
		/// Gets the last round.
		/// </summary>
		/// <value>The last round.</value>
		public Round CurrentRound { get { return _round; } }
	
		public LastRoundEvent (Round round)
		{
			_round = round;
		}
	}
}
