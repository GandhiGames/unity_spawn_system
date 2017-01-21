using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Example class reacting to round event \ref RoundManager.Events.PreperationEndEvent "PreperationEndEvent". \n
	/// Shows round countdown text on screen when \ref RoundManager.Events.PreperationEndEvent "PreperationEndEvent" is raised and the round starts.
	/// </summary>
	[RequireComponent (typeof(Text))]
	public class EnemiesRemainingText : MonoBehaviour
	{
		public string PreEnemiesText;
		public string PostEnemiesText;
		
		private Text _text;
		private Round _currentRound;
		
		void Awake ()
		{
			_text = GetComponent<Text> ();
			_text.enabled = false;
		}

		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<PreperationEndEvent> (OnRoundStart);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<PreperationEndEvent> (OnRoundStart);
		}	
		void Update ()
		{
			if (_currentRound != null) {
				
				_text.text = string.Format ("{0} {1} {2}", PreEnemiesText, _currentRound.RemainingEnemies, PostEnemiesText);
				
				if (_currentRound.RoundOver) {
					_currentRound = null;
					_text.enabled = false;
				}
			}
		}
		
		private void OnRoundStart (PreperationEndEvent e)
		{
			_text.enabled = true;
			_currentRound = e.CurrentRound;
		}
	}
}
