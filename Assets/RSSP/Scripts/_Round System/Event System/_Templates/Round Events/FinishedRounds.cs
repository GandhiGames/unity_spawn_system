using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Template class. Use this as a template to react to a \ref RoundManager.Events.FinishedRoundsEvent "FinishedRoundsEvent".
	/// This event is raised when all rounds are complete.
	/// Place your logic in the #OnFinishedRounds function.
	/// </summary>
	public class FinishedRounds : MonoBehaviour
	{
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<FinishedRoundsEvent> (OnFinishedRounds);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<FinishedRoundsEvent> (OnFinishedRounds);
		}
		
		/// <summary>
		/// React to \ref RoundManager.Events.FinishedRoundsEvent "FinishedRoundsEvent".
		/// </summary>
		/// <param name="e">Event.</param>
		public void OnFinishedRounds (FinishedRoundsEvent e)
		{
			
		}
		
	}
}
