using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Example class reacting to round event \ref RoundManager.Events.BossCountdownBegunEvent "BossCountdownBegunEvent". \n
	/// Shows boss countdown text on screen when \ref RoundManager.Events.BossCountdownBegunEvent "BossCountdownBegunEvent" is raised.
	/// </summary>
	[RequireComponent (typeof(Text))]
	public class BossCountdownText : MonoBehaviour
	{
		public string PreBossCountdownTimeText;
		public string PostBossCountdownText;
	
		private Text _text;
		private Round _currentRound;
		
		void Awake ()
		{
			_text = GetComponent<Text> ();
			_text.enabled = false;
		}
		
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<BossCountdownBegunEvent> (OnBossSpawnBegin);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<BossCountdownBegunEvent> (OnBossSpawnBegin);
		}
		
		void Update ()
		{
			if (_currentRound != null) {
				_text.text = string.Format ("{0} {1} {2}", PreBossCountdownTimeText, _currentRound.BossCountdown, PostBossCountdownText);
				
				if (_currentRound.BossCountdown <= 0) {
					_currentRound = null;
					_text.enabled = false;
				}
			}
		}
		
		private void OnBossSpawnBegin (BossCountdownBegunEvent e)
		{
			_text.enabled = true;
			_currentRound = e.CurrentRound;
		}
	}
}
