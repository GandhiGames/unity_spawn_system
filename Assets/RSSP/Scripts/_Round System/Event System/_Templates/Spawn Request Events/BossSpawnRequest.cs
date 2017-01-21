using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Template class. Use this as a template to react to a \ref RoundManager.Events.BossSpawnRequestEvent "BossSpawnRequestEvent".
	/// This event is raised when a boss should be spawned.
	/// Place your logic in the #OnBossSpawnRequest function.
	/// </summary>
	public class BossSpawnRequest : MonoBehaviour
	{
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<BossSpawnRequestEvent> (OnBossSpawnRequest);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<BossSpawnRequestEvent> (OnBossSpawnRequest);
		}
		
		/// <summary>
		/// React to \ref RoundManager.Events.BossSpawnRequestEvent "BossSpawnRequestEvent".
		/// Place your logic to spawn a boss here.
		/// </summary>
		/// <param name="e">Event.</param>
		private void OnBossSpawnRequest (BossSpawnRequestEvent e)
		{
			
		}
	}
}
