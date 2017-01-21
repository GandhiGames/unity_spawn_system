using UnityEngine;
using System.Collections;

namespace RoundManager.Events
{
	/// <summary>
	/// Raised everytime a round starts. 
	/// See RoundStart for a template of how to react to this event and AudioOnRoundStart
	/// for a concrete example.
	/// </summary>
	public class RoundStartEvent : RoundEvent
	{
		private Round _currentRound;
		/// <summary>
		/// Gets the round that raised the event.
		/// </summary>
		/// <value>The round.</value>
		public Round Round { get { return _currentRound; } }
		
		public RoundStartEvent (Round currentRound)
		{
			_currentRound = currentRound;
		}
	}
}
