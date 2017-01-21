using UnityEngine;
using System.Collections;

namespace RoundManager.Events
{
	/// <summary>
	/// Raised when the a checkpoint starts. See CheckPointStart template for an example of how to react to the event.
	/// </summary>
	public class CheckpointStartEvent : RoundEvent
	{
		private Round _currentRound;
		/// <summary>
		/// Gets the current round that raised this event.
		/// </summary>
		/// <value>The current round.</value>
		public Round CurrentRound { get { return _currentRound; } }
		
		public CheckpointStartEvent (Round currentRound)
		{
			_currentRound = currentRound;
		}
	}
}
