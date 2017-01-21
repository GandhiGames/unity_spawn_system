using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Template class. Use this as a template to react to a \ref RoundManager.Events.EnemySpawnRequestEvent "EnemySpawnRequestEvent".
	/// This event is raised when an enemy should be spawned.
	/// Place your logic in the #OnEnemySpawnRequest function.
	/// </summary>
	public class EnemySpawnRequest : MonoBehaviour
	{
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<EnemySpawnRequestEvent> (OnEnemySpawnRequest);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<EnemySpawnRequestEvent> (OnEnemySpawnRequest);
		}
		
		/// <summary>
		/// React to \ref RoundManager.Events.EnemySpawnRequestEvent "EnemySpawnRequestEvent".
		/// Place your logic to spawn an enemy here.
		/// </summary>
		/// <param name="e">Event.</param>
		private void OnEnemySpawnRequest (EnemySpawnRequestEvent e)
		{
			
		}
	}
}
