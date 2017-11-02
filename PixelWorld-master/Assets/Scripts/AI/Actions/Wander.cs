using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace AISystem
{
	public class Wander : Action 
	{
		public SharedVector3 target;

		private Enemy _owner;
		private Animator animator;
       		private NavMeshAgent agent;

		private NavMeshPath path = new NavMeshPath ();

       		private System.Random random;

		public override void OnAwake ()
		{
			_owner = gameObject.GetComponent<Enemy>();
			random = new System.Random();
			target.Value = transform.position;
		}

		public override void OnStart()
	    	{
			animator = GetComponent<Animator>();
			agent = GetComponent<NavMeshAgent>();
	   	}
		
		public override TaskStatus OnUpdate ()
		{
			Vector3 offset = target.Value - transform.position;
	           	offset.y = 0;

			if (offset.magnitude > 1) {

				if (agent.isOnNavMesh) {
					transform.forward = offset.normalized;

					bool hasPath = agent.CalculatePath (target.Value, path);
					if (path.status == NavMeshPathStatus.PathComplete && path.corners.Length > 1) {
						animator.SetBool("bMoving", true);
						//agent.SetPath (path);
						agent.SetDestination(target.Value);

						agent.avoidancePriority = 50;
					} else {
						agent.avoidancePriority = 40;
						animator.SetBool("bMoving", false);
						target.Value = transform.position;
						return TaskStatus.Failure;
					}
				}
			} else {

				if (agent.isOnNavMesh) {
					agent.ResetPath();
				}
				animator.SetBool("bMoving", false);
				agent.avoidancePriority = 40;
				float randomValue = (float)random.NextDouble();
				//if (randomValue < 0.5f) {
					target.Value = _owner.BornPosition + new Vector3(((float)random.NextDouble()-0.5f)*4, 0, ((float)random.NextDouble()-0.5f)*4);
				//}

				return TaskStatus.Failure;

			}
		
			return TaskStatus.Success;
		}
	}
}
