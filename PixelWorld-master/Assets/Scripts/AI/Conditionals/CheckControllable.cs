using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace AI {
	public class CheckControllable : Conditional 
	{
		private Character _owner;
		private Animator animator;
       		private NavMeshAgent agent;

		public override void OnAwake ()
		{
			_owner = gameObject.GetComponent<Character>();
		}

		public override void OnStart()
	    	{
			animator = GetComponent<Animator>();
			agent = GetComponent<NavMeshAgent>();
	   	}

		void Stop() {
			animator.SetBool("bAttack", false);
			animator.SetBool("bMoving", false);

			agent.ResetPath();
			agent.avoidancePriority = 40;
		}

		public override TaskStatus OnUpdate ()
		{
			if (_owner.HP <= 0) {
				Stop();
				return TaskStatus.Success;
			}

			if (_owner.IsControllable == false) {
				Stop();
				return TaskStatus.Success;
			}

			// buff
			if (_owner.IsStun || _owner.IsFrozen) {
				Stop();
				return TaskStatus.Success;
			}

			return TaskStatus.Failure;
		}
	}
}
