using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Template class. Use this as a template to react to a \ref RoundManager.Events.RoundEndEvent "RoundEndEvent".
	/// This event is raised at the end of each round.
	/// Place your logic in the #OnRoundEnd function.
	/// </summary>
	public class RoundEnd : MonoBehaviour
	{
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<RoundEndEvent> (OnRoundEnd);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<RoundEndEvent> (OnRoundEnd);
		}
		
		/// <summary>
		/// React to \ref RoundManager.Events.RoundEndEvent "RoundEndEvent".
		/// </summary>
		/// <param name="e">Event.</param>
		public void OnRoundEnd (RoundEndEvent e)
		{
			
		}
	}
}
