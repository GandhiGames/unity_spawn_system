using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Used by LightingChangeOnNewRound.
	/// Defines new light colour and intensity as well as the associated round number (defining when to change the lighting).
	/// </summary>
	[System.Serializable]
	public class RoundLighting
	{
		/// <summary>
		/// Round number to update lighting.
		/// </summary>
		public int RoundNumber;
		
		public Color LightingColour;
	
		[Range (0, 8)]
		public float
			LightIntensity;
	}
}
