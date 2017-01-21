using UnityEngine;
using System.Collections;

namespace RoundManager.Events
{
	/// <summary>
	/// Raised to signify that the current rounds preperation period has finished.
	/// See PreperationEnd for a template of how to react to this event.
	/// </summary>
	public class PreperationEndEvent : RoundEvent
	{
		private Round _currentRound;
		/// <summary>
		/// Gets the current round that raised this event.
		/// </summary>
		/// <value>The current round.</value>
		public Round CurrentRound { get { return _currentRound; } }
		
		public PreperationEndEvent (Round currentRound)
		{
			_currentRound = currentRound;
		}
	}
}
