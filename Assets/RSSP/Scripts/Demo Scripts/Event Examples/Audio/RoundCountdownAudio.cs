using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{

/// <summary>
/// Plays Audio Clip when Round has {SecondsLeftToBeginAudio} until it will begin. 
/// </summary>
	[RequireComponent (typeof(AudioSource))]
	public class RoundCountdownAudio : MonoBehaviour
	{
		public AudioClip Audio;
		public float Volume = 1;
		public int SecondsLeftToBeginAudio = 3;

		private AudioSource _audio;
		private Round _currentRound;

		void Awake ()
		{
			_audio = GetComponent<AudioSource> ();
		}
	
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<PreperationStartEvent> (OnPreperationStart);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<PreperationStartEvent> (OnPreperationStart);
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (_currentRound != null) {
				if (_currentRound.PreperationTime <= SecondsLeftToBeginAudio) {
					_audio.PlayOneShot (Audio, Volume);
					_currentRound = null;
				}
			}
	
		}
		
		private void OnPreperationStart (PreperationStartEvent e)
		{
			_currentRound = e.CurrentRound;
		}
	}
}
