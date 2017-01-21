using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Attach to any gameobject that is classed as a rounds boss. When the object attached to this script is disabled the current round is notified.
	/// </summary>
	public class RoundBoss : MonoBehaviour
	{
		private RoundManager _manager;
		
		void Awake ()
		{
			_manager = RoundManager.Instance;
		}
	
		void OnEnable ()
		{
			RoundEvents.Instance.Raise (new BossSpawnedEvent (_manager.CurrentRound, this.gameObject));
		}
	
		void OnDisable ()
		{
			_manager.CurrentRound.BossKilled ();
		}
	}
}
