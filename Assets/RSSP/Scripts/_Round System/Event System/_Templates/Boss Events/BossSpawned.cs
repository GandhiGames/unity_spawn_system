using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Template class. Use this as a template to react to a \ref RoundManager.Events.BossSpawnedEvent "BossSpawnedEvent".
	/// This event is raised when a rounds boss is spawned.
	/// Place your logic in the #OnBossSpawned function.
	/// </summary>
	public class BossSpawned : MonoBehaviour
	{
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<BossSpawnedEvent> (OnBossSpawned);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<BossSpawnedEvent> (OnBossSpawned);
		}
		
		/// <summary>
		/// React to \ref RoundManager.Events.BossSpawnedEvent "BossSpawnedEvent".
		/// </summary>
		/// <param name="e">Event.</param>
		public void OnBossSpawned (BossSpawnedEvent e)
		{
			
		}
	}
}
