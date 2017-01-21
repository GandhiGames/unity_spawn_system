using UnityEngine;
using System.Collections;

namespace RoundManager
{
	/// <summary>
	/// Used by BackgroundAudioChangeOnNewRound.
	/// Defines audio clips to play and their associated round number defining when to start the clip.
	/// </summary>
	[System.Serializable]
	public class BackgroundAudio
	{
		public int RoundNumber;
		public AudioClip Audio;
		
	}
}
