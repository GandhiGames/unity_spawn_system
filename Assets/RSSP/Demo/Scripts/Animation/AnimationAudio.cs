using UnityEngine;
using System.Collections;

namespace SurvivalKit
{
/// <summary>
/// Plays Random audio clip contained in AudioClips. Called my animation.
/// </summary>
	[RequireComponent (typeof(AudioSource))]
	public class AnimationAudio : MonoBehaviour
	{

		public AudioClip[] AudioClips;
	
		public float VolumeScale = 1f;
	
		[Range (0, 1f)]
		public float
			PlayChance = 0f;
	
		private bool soundEnabled;

		private AudioSource audioSource;
		
		void Awake ()
		{
			audioSource = GetComponent<AudioSource> ();
		}

		void Start ()
		{
			soundEnabled = AudioClips != null && AudioClips.Length > 0 && audioSource && audioSource.enabled;
		}
	
		public void PlaySound ()
		{

			if (soundEnabled && Random.Range (0, 1f) < PlayChance) {
				audioSource.PlayOneShot (AudioClips [Random.Range (0, AudioClips.Length)], VolumeScale);
			}
		}
	}
}
