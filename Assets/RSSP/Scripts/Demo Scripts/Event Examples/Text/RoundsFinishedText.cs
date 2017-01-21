using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Example class reacting to round event \ref RoundManager.Events.FinishedRoundsEvent "FinishedRoundsEvent". \n
	/// Shows #TextToShow on screen when \ref RoundManager.Events.FinishedRoundsEvent "FinishedRoundsEvent" is raised i.e. all rounds have finished.
	/// </summary>
	[RequireComponent (typeof(Text))]
	public class RoundsFinishedText : MonoBehaviour
	{
		public string TextToShow;
		
		public int ShowForSeconds = 1;
		
		private Text _text;
		
		void Awake ()
		{
			_text = GetComponent<Text> ();
			_text.enabled = false;
		}
		
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<FinishedRoundsEvent> (OnRoundsFinished);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<FinishedRoundsEvent> (OnRoundsFinished);
		}
		
		void OnRoundsFinished (FinishedRoundsEvent e)
		{
			_text.enabled = true;
			
			_text.text = TextToShow;
			
			StartCoroutine (DisableText ());
		}
		
		private IEnumerator DisableText ()
		{
			yield return new WaitForSeconds (ShowForSeconds);
			_text.enabled = false;
		}
	}
}
