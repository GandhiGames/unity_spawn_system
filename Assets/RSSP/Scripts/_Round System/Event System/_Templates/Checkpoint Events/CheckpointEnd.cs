using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Template class. Use this as a template to react to a \ref RoundManager.Events.CheckpointEndEvent "CheckpointEndEvent".
	/// This event is raised whenever a checkpoint finishes.
	/// Place your logic in the #OnCheckpointEnd function.
	/// </summary>
	public class CheckpointEnd : MonoBehaviour
	{
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<CheckpointEndEvent> (OnCheckpointEnd);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<CheckpointEndEvent> (OnCheckpointEnd);
		}
		
		/// <summary>
		/// React to \ref RoundManager.Events.CheckpointEndEvent "CheckpointEndEvent".
		/// </summary>
		/// <param name="e">Event.</param>
		public void OnCheckpointEnd (CheckpointEndEvent e)
		{
			
		}
	}
}
