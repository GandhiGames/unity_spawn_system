using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Example class reacting to round event \ref RoundManager.Events.RoundStartEvent "RoundStartEvent". \n
	/// Plays an audio clip when a round starts. 
	/// </summary>
	[RequireComponent (typeof(AudioSource))]
	public class AudioOnRoundStart : MonoBehaviour
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
			RoundEvents.Instance.AddListener<RoundStartEvent> (OnRoundStart);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<RoundStartEvent> (OnRoundStart);
		}
	
		private void OnRoundStart (RoundStartEvent e)
		{
			_audio.PlayOneShot (Audio, Volume);
		}
	}
}
