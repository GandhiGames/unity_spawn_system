using UnityEngine;
using System.Collections;

namespace RoundManager
{
	public class MachineGun : DamageTurret
	{
		private Animator _animator;
		private int firingHash = Animator.StringToHash ("firing");

		public override void Awake ()
		{
			_animator = GetComponent<Animator> ();
			_animator.speed = ShootSpeed;
			base.Awake ();
		}
	
		void Update ()
		{
			if (NoTargets ()) {
				return;
			}
		
			if (TargetDestroyed ()) {
				_animator.SetBool (firingHash, false);
				GetTarget ();
				return;
			}
		
			RotateTowardsTarget ();
		
			if (ClearPathToEnemy (transform.right)) {
				_animator.SetBool (firingHash, true);
			} else {
				_animator.SetBool (firingHash, false);
			
			}
		}

	}
}
