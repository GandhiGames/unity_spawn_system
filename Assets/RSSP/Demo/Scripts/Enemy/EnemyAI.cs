using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SurvivalKit
{
	[RequireComponent (typeof(Animator))]
	[RequireComponent (typeof(Rigidbody2D))]
	public class EnemyAI : MonoBehaviour
	{
		public float MoveSpeed;
		public float SightRadius = 5f;
		public float AttackDistance = 0.5f;

		protected Transform _currentTarget;

		protected bool canMove;
		public bool CanMove {
			set {
				canMove = value;
			}
		}
		
		protected Animator animator;
		protected int attackHash = Animator.StringToHash ("attacking");


		public virtual void Awake ()
		{
			animator = GetComponent<Animator> ();
			canMove = false;

			_currentTarget = GameObject.FindGameObjectWithTag ("Turret").transform;
		}

		public void SpawnComplete ()
		{
			canMove = true;
		}

		public virtual void Update ()
		{
			if (!canMove) {
				animator.SetBool (attackHash, false);
				return;
			}

			if (InAttackRange ()) {
				animator.SetBool (attackHash, true);
			} else {
				animator.SetBool (attackHash, false);
				MoveToTarget ();
			}
			
		}

		
		protected bool InAttackRange ()
		{
			return (_currentTarget.position - transform.position).magnitude <= AttackDistance;
		}
		
		protected void MoveToTarget ()
		{
			var heading = _currentTarget.position - transform.position;
			var distance = heading.magnitude;
			var dir = heading / distance;
			
			var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.Translate (dir * MoveSpeed * Time.deltaTime, Space.World);
		}
		

	}
}
