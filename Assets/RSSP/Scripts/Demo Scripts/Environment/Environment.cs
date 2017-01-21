using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace RoundManager
{
	/// <summary>
	/// Generates the demo scene environment.
	/// </summary>
	[ExecuteInEditMode]
	public class Environment : MonoBehaviour
	{
		public GameObject FloorPrefab;
		public Sprite[] FloorSprites;
		public Rect RoomSize;

		[SerializeField]
		private List<Transform>
			_objectSpawnTiles = new List<Transform> ();
		public List<Transform> ObjectSpawnTiles { get { return _objectSpawnTiles; } }

		[SerializeField]
		private List<Transform>
			_enemySpawnTiles = new List<Transform> ();
		public List<Transform> EnemySpawnTiles { get { return _enemySpawnTiles; } }


		/// <summary>
		/// The enemy spawn area consists of the blocks surrounding the floor area. This defines the size of the surrounding area.
		/// </summary>
		public Vector2i EnemySpawnArea = new Vector2i (3, 3);

		void Start ()
		{
			if (transform.childCount == 0)
				GenerateFloor ();
		}

		/// <summary>
		/// Generates the demo environment.
		/// </summary>
		public void GenerateFloor ()
		{
			var renderer = FloorPrefab.GetComponent<SpriteRenderer> ();
			float floorWidth = GetTileWidth (renderer.sprite);
			float floorHeight = GetTileHeight (renderer.sprite);
			
			var floorSpriteOptions = HasFloorSpriteOptions ();
			
			for (int i = 0; i < RoomSize.width; i++) {
				var x = RoomSize.x + i * floorWidth;
				for (int j = 0; j < RoomSize.height; j++) {
					var y = RoomSize.y + j * floorHeight;
					
					var position = new Vector2 (x, y);
					var tileClone = (GameObject)Instantiate (FloorPrefab, position, Quaternion.identity);
					tileClone.transform.SetParent (transform);
					
					if (floorSpriteOptions) {
						UpdateCellSprite (tileClone);
					}
					
					var coord = new Vector2i (i, j);

					if (coord.X <= EnemySpawnArea.X - 1 || coord.Y <= EnemySpawnArea.Y - 1 || 
						coord.X >= (RoomSize.width - EnemySpawnArea.X) || coord.Y >= (RoomSize.height - EnemySpawnArea.Y)) {
						_enemySpawnTiles.Add (tileClone.transform);
					} else {
						_objectSpawnTiles.Add (tileClone.transform);
					}
					
					
				}
			}
		}

		private void RemoveFloorIfPresent ()
		{
			if (transform.childCount > 0) {
				foreach (Transform t in transform) {
					Destroy (t.gameObject);
				}
			}
		}

		private void UpdateCellSprite (GameObject tileClone)
		{
			tileClone.GetComponent<SpriteRenderer> ().sprite = FloorSprites [Random.Range (0, FloorSprites.Length)];
		}
		
		private bool HasFloorSpriteOptions ()
		{
			return FloorSprites != null && FloorSprites.Length > 0;
		}

		private float GetTileWidth (Sprite tile)
		{
			return tile.bounds.size.x;
		}
	
		private float GetTileHeight (Sprite tile)
		{
			return tile.bounds.size.y;
		}

		

		
	}
}
