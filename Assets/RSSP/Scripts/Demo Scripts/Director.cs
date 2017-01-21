using UnityEngine;
using System.Collections;

namespace RoundManager
{
	/// <summary>
	/// Entry point for the demo scene.
	/// </summary>
	public class Director : MonoBehaviour
	{
		public Environment EnvironmentGenerator;

		void Start ()
		{
			RoundManager.Instance.Begin ();
		}
	
	}
}
