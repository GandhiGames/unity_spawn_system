using UnityEngine;
using System.Collections;

namespace SurvivalKit
{

	public class EnableParentMovement : MonoBehaviour
	{
		private EnemyAI movement;

		public void EnableMovement ()
		{
			if (!movement)
				movement = transform.parent.GetComponent<EnemyAI> ();

			movement.CanMove = true;
		}
	}
}
