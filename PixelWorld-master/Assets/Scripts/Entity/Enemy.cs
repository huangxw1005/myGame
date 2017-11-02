using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;

public class Enemy : Character {

	public Vector3 BornPosition {get;set;}

	BehaviorTree tree;
	NavMeshAgent agent;

	protected override void Awake() {
		base.Awake();

		tree = GetComponent<BehaviorTree>();
		agent = GetComponent<NavMeshAgent>();
		//agent.enabled = false;
	}

	protected override void Start() {
		base.Start();

		BornPosition = transform.position;
		//agent.enabled = true;
	}

	public override void ActDie() {
		base.ActDie();

		//characterController.enabled = false;
		tree.enabled = false;
		agent.Stop();
	}

	void OnTriggerEnter(Collider collider)   { 
		string tag = collider.gameObject.tag;
		Debug.Log("Enemy.OnTriggerEnter " + tag);  
		if (tag == "PlayerWeapon") {
			Player player = collider.transform.parent.GetComponent<Player>();
			Debug.Log("player " + player.ID);
			Vector3 offset = transform.position - player.transform.position;
			offset.y = 0;
			BattleManager.GetInstance ().EnemyHit (ID, player.ID);
			if (HP > 0) {
				ActHit();
				AttackEffect("Prefabs/Effect/Hit/Fx_hit", 1, new Vector3(0, 0.5f, 0));
				StartCoroutine(HitBack (offset.normalized));
			} else {
				StartCoroutine(HitFly (offset.normalized));
			}
		}
	}
	void OnTriggerExit(Collider collider)  {  
		//Debug.Log("OnTriggerExit");  
	}  
}
