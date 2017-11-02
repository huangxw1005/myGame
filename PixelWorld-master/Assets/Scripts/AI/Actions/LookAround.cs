using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace AISystem
{
	public class LookAround : Action 
	{
		private Enemy _owner;
		private Animator animator;
       		private NavMeshAgent agent;

       		private System.Random random;

		public override void OnAwake ()
		{
			_owner = gameObject.GetComponent<Enemy>();
			random = new System.Random();
		}

		public override void OnStart()
	    	{
			animator = GetComponent<Animator>();
	   	}
		
		public override TaskStatus OnUpdate ()
		{
			float angle = random.Next(0, 360);
			transform.eulerAngles = new Vector3(0, angle, 0);
			
			return TaskStatus.Success;
		}
	}
}
