using UnityEngine;
using System.Collections;

namespace RoundManager
{
	public class StandardGunProjectile : Projectile
	{
		public GameObject[] AnimationOnImpactPrefabs;
		
		public bool PlayAnimationOnWallHit = false;

		void OnTriggerEnter2D (Collider2D other)
		{
		
			if (other.CompareTag ("Enemy")) {
				InitDamageAnimation (other);
				ApplyDamage (other);
				if (DestroyOnEnemyImpact)
					ReturnProjectile ();
			}
		}
		
		protected void InitDamageAnimation (Collider2D other)
		{
			if (ImpactAnimationPresent ()) {
				var dir = transform.up.normalized;
				var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
				
				Instantiate (AnimationOnImpactPrefabs [Random.Range (0, AnimationOnImpactPrefabs.Length)], 
				             transform.position, Quaternion.AngleAxis (angle, Vector3.forward));
			}
		}
		
		private bool ImpactAnimationPresent ()
		{
			return AnimationOnImpactPrefabs != null && AnimationOnImpactPrefabs.Length > 0;
		}

	}
}
