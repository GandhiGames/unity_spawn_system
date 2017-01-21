using UnityEngine;
using System.Collections;

namespace SurvivalKit
{
	public class Disable : MonoBehaviour
	{

		public void ExecuteDisable ()
		{
			this.gameObject.SetActive (false);
		}
	}
}
