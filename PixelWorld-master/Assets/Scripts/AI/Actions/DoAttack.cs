﻿using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace AISystem
{
	public class DoAttack : Action 
	{
		public SharedVector3 direction;

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
			if (agent.hasPath) {
				agent.ResetPath();
			}
			agent.avoidancePriority = 30;
	        }
		
		public override TaskStatus OnUpdate ()
		{

			transform.forward = direction.Value;

			animator.SetBool("bMoving", false);

			animator.SetBool("bAttack", true);
			StartCoroutine(ResetValue());

			return TaskStatus.Success;
		}

		public IEnumerator ResetValue()
		{
			yield return null;
			animator.SetBool("bAttack", false);
		}
	}
}
