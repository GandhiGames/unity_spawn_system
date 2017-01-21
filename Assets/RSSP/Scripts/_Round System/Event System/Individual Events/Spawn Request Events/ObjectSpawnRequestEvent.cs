using UnityEngine;
using System.Collections;

namespace RoundManager.Events
{
	/// <summary>
	/// Raised when the an object should be spawned. See ObjectSpawnRequest template and
	/// ObjectSpawner for a concrete example of how to react to the event.
	/// </summary>
	public class ObjectSpawnRequestEvent : RoundEvent
	{
		private Round _currentRound;
		/// <summary>
		/// Gets the current round that raised the event.
		/// </summary>
		/// <value>The current round.</value>
		public Round CurrentRound { get { return _currentRound; } }
		
		private GameObject _objectPrefab;
		/// <summary>
		/// THe prefab that should be instantiated.
		/// </summary>
		/// <value>The object prefab.</value>
		public GameObject ObjectPrefab { get { return _objectPrefab; } }
		
		public ObjectSpawnRequestEvent (Round currentRound, GameObject objectPrefab)
		{
			_currentRound = currentRound;
			_objectPrefab = objectPrefab;
		}
	}
}
