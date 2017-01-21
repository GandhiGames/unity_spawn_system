using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Example class reacting to round event \ref RoundManager.Events.RoundStartEvent "RoundStartEvent". \n
	/// Changes background audio when a specific round is reached. Fades between previous audio clip and new clip.
	/// </summary>
	[RequireComponent (typeof(AudioSource))]
	public class BackgroundAudioChangeOnNewRound : MonoBehaviour
	{
		/// <summary>
		/// Defines audio clips to play and their associated round number (defining when to start the clip).
		/// </summary>
		public BackgroundAudio[] Audio;
		
		private AudioSource _audio;
		
		private AudioClip _audioClipTwo;
		private float _fadeSpeed = 1f;
		private bool _firstPlay = true;
		
		void Awake ()
		{
			_audio = GetComponent<AudioSource> ();
		}
		
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<RoundStartEvent> (OnNewRound);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.AddListener<RoundStartEvent> (OnNewRound);
		}

		void Update ()
		{
			if (_audioClipTwo) {
				if (_fadeSpeed < 0) {
					_audio.volume = Mathf.Max (_audio.volume + _fadeSpeed * Time.deltaTime, 0);
		
					if (_audio.volume == 0) {
						_audio.clip = _audioClipTwo;
						_audio.Play ();
						FadeIn ();
					}
				} else if (_fadeSpeed > 0) {
					_audio.volume = Mathf.Min (_audio.volume + _fadeSpeed * Time.deltaTime, 1);
					
					if (_audio.volume == 1) {
						_audioClipTwo = null;
					}
				}
			}
		}
		
		private void FadeOut ()
		{
			_fadeSpeed = -1f;
		}
		
		private void FadeIn ()
		{
			_fadeSpeed = 1f;
		}
		
		private void OnNewRound (RoundStartEvent e)
		{
			foreach (var audio in Audio) {
				if (e.Round.Manager.CurrentRoundIndex + 1 == audio.RoundNumber) {
				
					if (_firstPlay) {
						_audio.clip = audio.Audio;
						_audio.loop = true;
						_audio.Play ();
						_firstPlay = false;
					} else {
						_audioClipTwo = audio.Audio;
						FadeOut ();
					}
					
					break;
				}
			}
		}

	}
}
