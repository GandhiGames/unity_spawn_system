using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Template class. Use this as a template to react to a \ref RoundManager.Events.LastRoundEvent "LastRoundEvent".
	/// This event is raised at the beginning of the last round.
	/// Place your logic in the #OnLastRound function.
	/// </summary>
	public class LastRound : MonoBehaviour
	{
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<LastRoundEvent> (OnLastRound);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<LastRoundEvent> (OnLastRound);
		}
		
		/// <summary>
		/// React to \ref RoundManager.Events.LastRoundEvent "LastRoundEvent".
		/// </summary>
		/// <param name="e">Event.</param>
		private void OnLastRound (LastRoundEvent e)
		{
			
		}
		
	}
}
