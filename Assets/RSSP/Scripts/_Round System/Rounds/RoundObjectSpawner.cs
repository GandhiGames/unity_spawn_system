using UnityEngine;
using System.Collections;

/// <summary>
/// Round object spawner. Includes an object prefab and an associated spawn weight.
/// Used by RoundCheckpoint::RoundEnemies and Round::PreperationTimeObjects.
/// </summary>
[System.Serializable]
public class RoundObjectSpawner
{
	/// <summary>
	/// The prefab.
	/// </summary>
	public GameObject Prefab;
	
	/// <summary>
	/// The weight of the object prefab. Higher weights result
	/// in a greater chance that the #Prefab will be spawned.
	/// </summary>
	public float Weight;
}
