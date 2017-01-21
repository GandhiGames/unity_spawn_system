using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RoundManager.Events;

namespace RoundManager
{
	/// <summary>
	/// Main round manager class. Responsible for storing and updating rounds, and transitioning from one round to the next. 
	/// </summary>
	public class RoundManager : MonoBehaviour
	{
		/// <summary>
		/// Enables debug logging.
		/// </summary>
		public bool ShowDebugMessages;

		/// <summary>
		/// List of rounds.
		/// </summary>
		public Round[] Rounds;
		
		/// <summary>
		/// Returns the current active round.
		/// </summary>
		/// <value>The current round.</value>
		public Round CurrentRound { 
			get {
				if (_currentRound != -1) {
					return Rounds [_currentRound];
				}
				
				return null;
			}
		}

		private int _currentRound = -1;
		/// <summary>
		/// Gets the index of the current round from #Rounds.
		/// </summary>
		/// <value>The index of the current round.</value>
		public int CurrentRoundIndex { get { return _currentRound; } }
		
		private bool _roundsFinished = true;
		private bool _roundStarting = true;

		public delegate IEnumerator Routine ();

		private static RoundManager _instance;
		
		/// <summary>
		/// Gets an instance of RoundManager. Class can be accessed 
		/// from any script using: RoundManager.Instance;
		/// </summary>
		/// <value>The instance.</value>
		public static RoundManager Instance { 
			get { 
			
				if (!_instance) {
					_instance = GameObject.FindObjectOfType<RoundManager> ();
					
					if (!_instance.RoundsInitialised ()) {
						Debug.LogError ("Round system not intialised");
					}
				}
				
				return _instance; 
			} 
		}

		void Update ()
		{
			if (_roundsFinished || _roundStarting)
				return;
			
			if (Rounds [_currentRound].RoundOver) {					
				StartNextRound ();
				return;
			}
			
			Rounds [_currentRound].Execute ();
		}
		
		/// <summary>
		/// Entry point into the round spawing system. Starts round management and begins first round.
		/// Raises \ref RoundManager.Events.FirstRoundEvent "FirstRoundEvent".
		/// </summary>
		public void Begin ()
		{		
			if (RoundsInitialised ()) {
				_currentRound = -1;
				_roundsFinished = false;
				StartNextRound ();
				RoundEvents.Instance.Raise (new FirstRoundEvent (CurrentRound));
			}
		}
		
		/// <summary>
		/// Helper method to start Coroutine as Round and RoundCheckpoint do not derive from Monobehaviour
		/// and therefore cannot start coroutines.
		/// </summary>
		/// <param name="callBack">Call back.</param>
		public void CoroutineCallback (Routine callBack)
		{
			StartCoroutine (callBack ());
		}

		/// <summary>
		/// Returns true if last round.
		/// </summary>
		/// <returns><c>true</c>, if current round is last in list, <c>false</c> otherwise.</returns>
		public bool LastRound ()
		{
			return _currentRound == Rounds.Length - 1;
		}
		
		private void StartNextRound ()
		{
			if (_currentRound != -1) {
				CurrentRound.Exit ();
			}
			
			if (LastRound ()) {
				LogRoundMessage ("Finished all rounds");
				RoundEvents.Instance.Raise (new FinishedRoundsEvent (Rounds [_currentRound]));
				OnRoundsFinished ();
				return;
			}
			
			_currentRound++;
			
			if (LastRound ()) {
				RoundEvents.Instance.Raise (new LastRoundEvent (CurrentRound));
			}
			
			LogRoundMessage ("Starting new round");
			
			_roundStarting = true;
			
			Rounds [_currentRound].Enter (this);
			
			StartRoundPreparation ();
			
		}

		private void OnRoundsFinished ()
		{
			_roundsFinished = true;
		}

		private void StartRoundPreparation ()
		{
			if (Rounds [_currentRound].HasPreperationTime) {
				StartCoroutine (UpdatePreperationTimeCountdown ());
			} else {
				Rounds [_currentRound].OnPreparationOver ();
				_roundStarting = false;
			}
		}
		
		private IEnumerator UpdatePreperationTimeCountdown ()
		{
			Rounds [_currentRound].OnPreperationStart ();

			do {
				Rounds [_currentRound].PreperationTime--;
				yield return new WaitForSeconds (1f);
			} while (Rounds [_currentRound].PreperationTime > 0);

			Rounds [_currentRound].OnPreparationOver ();

			_roundStarting = false;
		}	
		
		#region Round Helper Methods
		private bool RoundsInitialised ()
		{
			return Rounds != null && Rounds.Length > 0;
		}
		
		private bool HasNextRound ()
		{
			return Rounds.Length >= (_currentRound + 1);
		}
		#endregion

		/// <summary>
		/// Helper method. Logs debug message if #ShowDebugMessages is true.
		/// </summary>
		/// <param name="message">Message.</param>
		public void LogRoundMessage (object message)
		{
			if (ShowDebugMessages) {
				Debug.Log (message);
			}
		}
	}
}
