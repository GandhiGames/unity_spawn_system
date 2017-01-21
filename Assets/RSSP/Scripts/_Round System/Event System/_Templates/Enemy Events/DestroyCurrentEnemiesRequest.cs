using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Template class. Use this as a template to react to a \ref RoundManager.Events.DestroyCurrentEnemiesRequestEvent "DestroyCurrentEnemiesRequestEvent".
	/// This event is raised when all currently spawned enemies should be killed.
	/// Place your logic in the #OnDestroyCurrentEnemiesRequest function.
	/// </summary>
	public class DestroyCurrentEnemiesRequest : MonoBehaviour
	{
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<DestroyCurrentEnemiesRequestEvent> (OnDestroyCurrentEnemiesRequest);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<DestroyCurrentEnemiesRequestEvent> (OnDestroyCurrentEnemiesRequest);
		}
		
		/// <summary>
		/// React to \ref RoundManager.Events.DestroyCurrentEnemiesRequestEvent "DestroyCurrentEnemiesRequestEvent".
		/// Place your logic here to destroy/disable all spawned enemies.
		/// </summary>
		/// <param name="e">Event.</param>
		public void OnDestroyCurrentEnemiesRequest (DestroyCurrentEnemiesRequestEvent e)
		{
			
		}
	}
}
