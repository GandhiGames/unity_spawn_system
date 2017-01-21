using UnityEngine;
using System.Collections;

namespace RoundManager
{
	/// <summary>
	/// Example enemy health script.
	/// </summary>
	[RequireComponent (typeof(AudioSource))]
	public class EnemyHealth : MonoBehaviour
	{
		public int MaxHealth;
		public AudioClip[] OnHitSounds;
		public GameObject OnDeadAnimation;
		public GameObject[] OnDeadSprites;
		public GameObject[] CollectiblesPrefabs;
		public int MinCollectiblesDropped;
		public int MaxCollectiblesDropped;

	
		private AudioSource _audio;
		private int _currentHealth;
	
		void Awake ()
		{
			_audio = GetComponent<AudioSource> ();
		}

		void OnEnable ()
		{
			_currentHealth = MaxHealth;
		}

		public void OnHit (int amount)
		{
			_currentHealth -= amount;

			if (_currentHealth <= 0) {
				OnDead ();
			}
		}


		public void OnDead ()
		{
			if (OnDeadAnimation) {
				Instantiate (OnDeadAnimation, transform.position, Quaternion.identity);
			}

			if (CollectiblesPrefabs != null && CollectiblesPrefabs.Length > 0) {
				var numOfCollectibles = Random.Range (MinCollectiblesDropped, MaxCollectiblesDropped + 1);

				for (int i = 0; i < numOfCollectibles; i++) {
					var pos = new Vector2 (transform.position.x + Random.Range (-0.5f, 0.5f), transform.position.y + Random.Range (-0.5f, 0.5f));
					Instantiate (CollectiblesPrefabs [Random.Range (0, CollectiblesPrefabs.Length)], pos, Quaternion.identity);
				}
			}


			Instantiate (OnDeadSprites [Random.Range (0, OnDeadSprites.Length)], transform.position, Quaternion.identity);
		
			Destroy (gameObject);

		}
		
		private void PlayHitSound ()
		{
			if (OnHitSounds != null && OnHitSounds.Length > 0 && _audio)
				_audio.PlayOneShot (OnHitSounds [Random.Range (0, OnHitSounds.Length)]);
		}
	}
}

