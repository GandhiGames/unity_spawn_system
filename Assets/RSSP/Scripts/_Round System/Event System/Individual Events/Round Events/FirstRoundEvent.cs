using UnityEngine;
using System.Collections;

namespace RoundManager.Events
{
	/// <summary>
	/// Raised to signify that the first round has started.
	/// See FirstRound for a template of how to react to this event.
	/// </summary>
	public class FirstRoundEvent : RoundEvent
	{
		private Round _round;
		/// <summary>
		/// Gets the first round.
		/// </summary>
		/// <value>The first round.</value>
		public Round CurrentRound { get { return _round; } }
		
		public FirstRoundEvent (Round round)
		{
			_round = round;
		}
	}
}
