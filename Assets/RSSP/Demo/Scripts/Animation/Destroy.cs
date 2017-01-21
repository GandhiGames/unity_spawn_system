using UnityEngine;
using System.Collections;

namespace SurvivalKit
{
	public class Destroy : MonoBehaviour
	{

		public void ExecuteDestroy ()
		{
			Destroy (this.gameObject);
		}
	}
}
