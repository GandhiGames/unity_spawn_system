using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Example class reacting to round event \ref RoundManager.Events.RoundEndEvent "RoundEndEvent". \n
	/// Plays an audio clip when a round ends. 
	/// </summary>
	[RequireComponent (typeof(AudioSource))]
	public class AudioOnRoundEnd : MonoBehaviour
	{
		public AudioClip Audio;
		public float Volume = 1f;
		
		private AudioSource _audio;
		
		void Awake ()
		{
			_audio = GetComponent<AudioSource> ();
		}
		
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<RoundEndEvent> (OnRoundEnd);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<RoundEndEvent> (OnRoundEnd);
		}
		
		private void OnRoundEnd (RoundEndEvent e)
		{
			_audio.PlayOneShot (Audio, Volume);
		}
	}
}
