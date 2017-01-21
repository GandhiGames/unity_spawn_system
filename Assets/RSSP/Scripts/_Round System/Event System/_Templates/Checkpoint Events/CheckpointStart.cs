using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Template class. Use this as a template to react to a \ref RoundManager.Events.CheckpointStartEvent "CheckpointStartEvent".
	/// This event is raised whenever a checkpoint starts.
	/// Place your logic in the #OnCheckpointStart function.
	/// </summary>
	public class CheckpointStart : MonoBehaviour
	{
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<CheckpointStartEvent> (OnCheckpointStart);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<CheckpointStartEvent> (OnCheckpointStart);
		}
		
		/// <summary>
		/// React to \ref RoundManager.Events.CheckpointStartEvent "CheckpointStartEvent".
		/// </summary>
		/// <param name="e">Event.</param>
		public void OnCheckpointStart (CheckpointStartEvent e)
		{
			
		}
	}
}
