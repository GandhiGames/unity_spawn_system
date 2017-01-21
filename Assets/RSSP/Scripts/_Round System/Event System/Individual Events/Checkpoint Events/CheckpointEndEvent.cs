using UnityEngine;
using System.Collections;

namespace RoundManager.Events
{
	/// <summary>
	/// Raised when the a checkpoint ends. See CheckPointEnd template for an example of how to react to the event.
	/// </summary>
	public class CheckpointEndEvent : RoundEvent
	{
		private Round _currentRound;
		/// <summary>
		/// Gets the current round that raised this event.
		/// </summary>
		/// <value>The current round.</value>
		public Round CurrentRound { get { return _currentRound; } }

		private bool _hasNextCheckpoint;
		/// <summary>
		/// A value indicating whether there is another checkpoint after this one.
		/// </summary>
		/// <value><c>true</c> if this instance has next checkpoint; otherwise, <c>false</c>.</value>
		public bool HasNextCheckpoint { get { return _hasNextCheckpoint; } }
		
		public CheckpointEndEvent (Round currentRound, bool hasNextCheckpoint)
		{
			_currentRound = currentRound;
			_hasNextCheckpoint = hasNextCheckpoint;
		}
	}
}
