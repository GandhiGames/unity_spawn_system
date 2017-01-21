using UnityEngine;
using System.Collections;

namespace RoundManager
{
	/// <summary>
	/// Example class triggering the end of a round. \n
	/// Invokes Round#TriggerRoundEnd() when an object with tag 'Player' enters trigger.
	/// Can be used to end a round when player reaches specific point.
	/// </summary>
	[RequireComponent (typeof(Collider))]
	public class TriggerRoundEnd : MonoBehaviour
	{
		void OnTriggerEnter (Collider other)
		{
			if (other.CompareTag ("Player")) {
				RoundManager.Instance.CurrentRound.TriggerRoundEnd ();
			}
		}
		
	}
}
