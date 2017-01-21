using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Example class reacting to round event \ref RoundManager.Events.PreperationStartEvent "PreperationStartEvent". \n
	/// Shows round preperation countdown text on screen when \ref RoundManager.Events.PreperationStartEvent "PreperationStartEvent" is raised.
	/// </summary>
	[RequireComponent (typeof(Text))]
	public class PreperationCountdownText : MonoBehaviour
	{
		public string PrePreperationTimeText;
		public string PostPreperationTimeText;
	
		private Text _text;
		private Round _currentRound;
		
		void Awake ()
		{
			_text = GetComponent<Text> ();
			_text.enabled = false;
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
				_text.text = string.Format ("{0} {1} {2}", PrePreperationTimeText, _currentRound.PreperationTime, PostPreperationTimeText);
				
				if (!_currentRound.InPreperation) {
					_currentRound = null;
					_text.enabled = false;
				}
			}
		}
		
		void OnPreperationStart (PreperationStartEvent e)
		{
			_currentRound = e.CurrentRound;
			_text.enabled = true;
		}
		
		
	}
}
