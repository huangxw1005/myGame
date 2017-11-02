using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace AISystem
{
	public class MoveToTarget : Action 
	{
		public SharedVector3 target;
		public SharedVector3 direction;

		private Character _owner;
       		private Animator animator;
		private CharacterController characterController;
		private NavMeshAgent agent;
		private NavMeshPath path = new NavMeshPath ();

		public override void OnAwake ()
		{
			_owner = gameObject.GetComponent<Character>();
		}
		public override void OnStart()
	        {
			animator = GetComponent<Animator>();
			characterController = GetComponent<CharacterController>();
			agent = GetComponent<NavMeshAgent>();
	        }
		public override TaskStatus OnUpdate ()
		{
	
			transform.forward = direction.Value;
			Vector3 move =  _owner.Speed * direction.Value * Time.deltaTime;
			move.y = -10;
			//characterController.Move(move);

			bool hasPath = agent.CalculatePath (target.Value, path);
			if (path.status == NavMeshPathStatus.PathComplete && path.corners.Length > 0) {
				agent.SetPath (path);
				agent.avoidancePriority = 50;
			} else {
				agent.avoidancePriority = 40;
				target.Value = transform.position;
				return TaskStatus.Failure;
			}
			//agent.SetDestination(target.Value);

			animator.SetBool("bAttack", false);
			animator.SetBool("bMoving", true);

			return TaskStatus.Success;
		}

	}
}
