using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Example class reacting to round event \ref RoundManager.Events.RoundStartEvent "RoundStartEvent". \n
	/// Shows round number on screen when \ref RoundManager.Events.RoundStartEvent "RoundStartEvent".
	/// </summary>
	[RequireComponent (typeof(Text))]
	public class ShowTextOnRoundStart : MonoBehaviour
	{
		public string PreRoundNumberText;
		public string PostRoundNumberText;
	
		public int ShowForSeconds = 1;
		
		private Text _text;
		
		void Awake ()
		{
			_text = GetComponent<Text> ();
			_text.enabled = false;
		}
	
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<RoundStartEvent> (OnRoundStart);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<RoundStartEvent> (OnRoundStart);
		}
		
		void OnRoundStart (RoundStartEvent e)
		{
			_text.enabled = true;
			
			_text.text = string.Format ("{0} {1} {2}", PreRoundNumberText, e.Round.Manager.CurrentRoundIndex + 1, PostRoundNumberText);
			
			StartCoroutine (DisableText ());
		}
		
		private IEnumerator DisableText ()
		{
			yield return new WaitForSeconds (ShowForSeconds);
			_text.enabled = false;
		}
	}
}
