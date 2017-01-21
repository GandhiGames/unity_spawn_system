using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Template class. Use this as a template to react to a \ref RoundManager.Events.FirstRoundEvent "FirstRoundEvent".
	/// This event is raised at the beginning of the first round.
	/// Place your logic in the #OnFirstRound function.
	/// </summary>
	public class FirstRound : MonoBehaviour
	{
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<FirstRoundEvent> (OnFirstRound);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<FirstRoundEvent> (OnFirstRound);
		}
		
		/// <summary>
		/// React to \ref RoundManager.Events.FirstRoundEvent "FirstRoundEvent".
		/// </summary>
		/// <param name="e">Event.</param>
		public void OnFirstRound (FirstRoundEvent e)
		{
			
		}
		
	}
}
