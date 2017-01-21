using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Template class. Use this as a template to react to a \ref RoundManager.Events.BossCountdownBegunEvent "BossCountdownBegunEvent".
	/// This event is raised whenever a boss countdown begins.
	/// Place your logic in the #OnBossCountdownBegun function.
	/// </summary>
	public class BossCountdownBegun : MonoBehaviour
	{
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<BossCountdownBegunEvent> (OnBossCountdownBegun);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<BossCountdownBegunEvent> (OnBossCountdownBegun);
		}
		
		/// <summary>
		/// React to \ref RoundManager.Events.BossCountdownBegunEvent "BossCountdownBegunEvent".
		/// </summary>
		/// <param name="e">Event.</param>
		public void OnBossCountdownBegun (BossCountdownBegunEvent e)
		{
		
		}
	}
}
