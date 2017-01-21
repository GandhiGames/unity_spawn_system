using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Template class. Use this as a template to react to a \ref RoundManager.Events.BossKilledEvent "BossKilledEvent".
	/// This event is raised whenever a boss is killed.
	/// Place your logic in the #OnBossKilled function.
	/// </summary>
	public class BossKilled : MonoBehaviour
	{
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<BossKilledEvent> (OnBossKilled);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<BossKilledEvent> (OnBossKilled);
		}
		
		/// <summary>
		/// React to \ref RoundManager.Events.BossKilledEvent "BossKilledEvent".
		/// </summary>
		/// <param name="e">Event.</param>
		public void OnBossKilled (BossKilledEvent e)
		{
			
		}
	}
}
