using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Template class. Use this as a template to react to a \ref RoundManager.Events.ObjectSpawnRequestEvent "ObjectSpawnRequestEvent".
	/// This event is raised when a object should be spawned. This is raised during the preperation stage of a round if the round has Round#PreperationTimeObjects.
	/// Place your logic in the #OnObjectSpawnRequest function.
	/// </summary>
	public class ObjectSpawnRequest : MonoBehaviour
	{
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<ObjectSpawnRequestEvent> (OnObjectSpawnRequest);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<ObjectSpawnRequestEvent> (OnObjectSpawnRequest);
		}
		
		/// <summary>
		/// React to \ref RoundManager.Events.ObjectSpawnRequestEvent "ObjectSpawnRequestEvent".
		/// Place logic to spawn preperation objects here.
		/// </summary>
		/// <param name="e">Event.</param>
		public void OnObjectSpawnRequest (ObjectSpawnRequestEvent e)
		{
			
		}
	}
}
