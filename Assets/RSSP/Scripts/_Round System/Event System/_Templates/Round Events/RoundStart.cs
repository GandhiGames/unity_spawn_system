using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Template class. Use this as a template to react to a \ref RoundManager.Events.RoundStartEvent "RoundStartEvent".
	/// This event is raised at the start of each round.
	/// Place your logic in the #OnRoundStart function.
	/// </summary>
	public class RoundStart : MonoBehaviour
	{
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<RoundStartEvent> (OnRoundStart);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<RoundStartEvent> (OnRoundStart);
		}
		
		/// <summary>
		/// React to \ref RoundManager.Events.RoundStartEvent "RoundStartEvent".
		/// </summary>
		/// <param name="e">Event.</param>
		public void OnRoundStart (RoundStartEvent e)
		{
			
		}
	}
}
