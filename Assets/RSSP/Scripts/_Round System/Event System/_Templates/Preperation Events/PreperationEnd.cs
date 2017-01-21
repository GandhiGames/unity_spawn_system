using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Template class. Use this as a template to react to a \ref RoundManager.Events.PreperationEndEvent "PreperationEndEvent".
	/// This event is raised when a rounds preperation stage has finished.
	/// Place your logic in the #OnPreperationEnd function.
	/// </summary>
	public class PreperationEnd : MonoBehaviour
	{
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<PreperationEndEvent> (OnPreperationEnd);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<PreperationEndEvent> (OnPreperationEnd);
		}
		
		/// <summary>
		/// React to \ref RoundManager.Events.PreperationEndEvent "PreperationEndEvent".
		/// </summary>
		/// <param name="e">Event.</param>
		public void OnPreperationEnd (PreperationEndEvent e)
		{
			
		}
		
	}
}
