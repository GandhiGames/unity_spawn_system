using UnityEngine;
using System.Collections;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Example class reacting to round event \ref RoundManager.Events.RoundStartEvent "RoundStartEvent". \n
	/// Changes scene lighting colour and intensity when a specific round is reached. Lerps between previous and new colour/intensity.
	/// </summary>
	[RequireComponent (typeof(Light))]
	public class LightingChangeOnNewRound : MonoBehaviour
	{
		/// <summary>
		/// Defines new light colour and intensity as well as the associated round number (defining when to change the lighting).
		/// </summary>
		public RoundLighting[] Lighting;
		
		private Light _light;
		private RoundLighting _currentLighting;
		private Color _previousLightColour;
		private float _previousLightIntensity;
		private LerpOverTime _lerp;
		
		void Awake ()
		{
			_light = GetComponent<Light> ();
		}
		
		void OnEnable ()
		{
			RoundEvents.Instance.AddListener<RoundStartEvent> (OnRoundChange);
		}
		
		void OnDisable ()
		{
			RoundEvents.Instance.RemoveListener<RoundStartEvent> (OnRoundChange);
		}
		
		void Update ()
		{
			if (_currentLighting != null) {
				_light.intensity = Mathf.Lerp (_previousLightIntensity, _currentLighting.LightIntensity, _lerp.Value);
				_light.color = Color.Lerp (_previousLightColour, _currentLighting.LightingColour, _lerp.Value);
			}
		}
		
		private void OnRoundChange (RoundStartEvent e)
		{
			foreach (var lighting in Lighting) {
				if (e.Round.Manager.CurrentRoundIndex + 1 == lighting.RoundNumber) {
					_currentLighting = lighting;

					_previousLightColour = _light.color;
					_previousLightIntensity = _light.intensity;
					
					_lerp = new LerpOverTime (0, 1, e.Round.RoundTotalTime);
					_lerp.Start ();
					break;
				}
			}
	
		}
		
	}
}
