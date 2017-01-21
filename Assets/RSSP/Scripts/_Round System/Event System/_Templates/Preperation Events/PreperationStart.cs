using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Template class. Use this as a template to react to a \ref RoundManager.Events.PreperationStartEvent "PreperationStartEvent".
	/// This event is raised when a rounds preperation stage starts.
	/// Place your logic in the #OnPreperationStart function.
	/// </summary>
	public class PreperationStart : MonoBehaviour
	{
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<PreperationStartEvent> (OnPreperationStart);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<PreperationStartEvent> (OnPreperationStart);
		}
		
		/// <summary>
		/// React to \ref RoundManager.Events.PreperationStartEvent "PreperationStartEvent".
		/// </summary>
		/// <param name="e">Event.</param>
		public void OnPreperationStart (PreperationStartEvent e)
		{
			
		}
		
	}
}