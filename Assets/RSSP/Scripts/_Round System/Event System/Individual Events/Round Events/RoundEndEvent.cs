using UnityEngine;
using System.Collections;

namespace RoundManager.Events
{
	/// <summary>
	/// Raised everytime a round ends. 
	/// See RoundEnd for a template of how to react to this event and AudioOnRoundEnd
	/// for a concrete example.
	/// </summary>
	public class RoundEndEvent : RoundEvent
	{
		private Round _currentRound;
		/// <summary>
		/// Gets the current round that raised the event.
		/// </summary>
		/// <value>The current round.</value>
		public Round CurrentRound { get { return _currentRound; } }
		
		private bool _hasNextRound;
		/// <summary>
		/// Gets a value indicating whether this instance has next round.
		/// </summary>
		/// <value><c>true</c> if this instance has next round; otherwise, <c>false</c>.</value>
		public bool HasNextRound { get { return _hasNextRound; } }
		
		public RoundEndEvent (Round currentRound, bool hasNextRound)
		{
			_currentRound = currentRound;
			_hasNextRound = hasNextRound;
		}
	}
}
