using UnityEngine;
using System.Collections;

namespace RoundManager
{
	/// <summary>
	/// Attach to any gameobject that is classed as a round enemy. It registers with the current round when an enemy is spawned and killed. Used for when
	/// you want he player to have killed all enemies before the round boss is spawned.
	/// </summary>
	public class RoundEnemy : MonoBehaviour
	{
		private RoundManager _manager;
		
		void Awake ()
		{
			_manager = RoundManager.Instance;
		}
		
		void OnEnable ()
		{
			_manager.CurrentRound.RegisterEnemySpawned ();
		}
		
		void OnDisable ()
		{
			_manager.CurrentRound.RegisterEnemyKilled ();
		}
	}
}
