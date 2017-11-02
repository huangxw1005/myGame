using UnityEngine;
using System.Collections;

public class Player : Character {

	//角色控制器
	private CharacterController m_Controller;

	private int m_AttackIdx = 1;

	// Use this for initialization
	protected override void Awake () {
		base.Awake();

		m_Controller=GetComponent<CharacterController>();
	}

	public virtual void ActAttack1() {
		Debug.Log("ActAttack1");
		switch(m_CharacterState) {
		case CharaterState.ATTACK1_1:
			m_AttackIdx = 2;
			break;
		case CharaterState.ATTACK1_2:
			m_AttackIdx = 3;
			break;
		case CharaterState.ATTACK1_3:
			m_AttackIdx = 1;
			break;
		default:
			m_AttackIdx = 1;
			m_CharacterState = CharaterState.ATTACK1_1;
			m_Animator.CrossFade("attack1_1", 0);
			break;
		}
	}

	private void ActAttack1_2() {
		m_CharacterState = CharaterState.ATTACK1_2;
		m_Animator.CrossFade ("attack1_2", 0);
	}
	private void ActAttack1_3() {
		m_CharacterState = CharaterState.ATTACK1_3;
		m_Animator.CrossFade ("attack1_3", 0);
	}

	public void OnAnimationComplete() {
		Debug.LogFormat("amation state:{0} complete", m_CharacterState);

		if (m_CharacterState == CharaterState.ATTACK1_1 && m_AttackIdx > 1) {
			m_Controller.Move(transform.forward*0.5f);
			ActAttack1_2();
		} else if (m_CharacterState == CharaterState.ATTACK1_2 && m_AttackIdx > 2) {
			m_Controller.Move(transform.forward*0.5f);
			ActAttack1_3();
		} else {
			ActIdle();
		}
	}

	// auto-rotate
	public void AutoRotateToEnemy() {
		float distance = 0;
		Enemy enemy = CharacterManager.Instance.FindNearestEnemy(transform.position, out distance);
		if (distance < DistAttack*3) {
			Vector3 offset = enemy.transform.position - transform.position;
			offset.y = 0;
			transform.forward = offset.normalized;
		}
	}

	protected override void StartAttack ()
	{
		base.StartAttack ();


	}

	protected void StartSkill1 ()
	{
		// missile
		GameObject prefab = (GameObject)ResourceManager.Instance.LoadAsset("Prefabs/Effect/Skill/Fx_arrow");
		ObjectPool.CreatePool("efx_arrow", prefab, 1);
		GameObject go = ObjectPool.Spawn("efx_arrow");
		go.tag = gameObject.tag + "Missile";
		go.transform.localScale = Vector3.one;
		go.transform.forward = transform.forward;
		go.transform.position = transform.position + new Vector3(0, 0.5f, 0) + transform.forward;
		Missile missile = go.GetComponent<Missile>();
	}

	void OnTriggerEnter(Collider collider)   { 
		string tag = collider.gameObject.tag;
		Debug.Log("Player.OnTriggerEnter " + tag);  
		if ( tag == "EnemyWeapon") {
			Enemy enemy = collider.transform.parent.GetComponent<Enemy>();
			Debug.Log("Enemy " + enemy.ID);
			ActHit();
			AttackEffect("Prefabs/Effect/Hit/Fx_hit", 1, new Vector3(0, 0.5f, 0));
			Vector3 offset = transform.position - enemy.transform.position;
			offset.y = 0;
			StartCoroutine(HitBack (offset.normalized));
			BattleManager.GetInstance ().PlayerHit (ID, enemy.ID);
		} else if (tag == "NPC") {
			NPC npc = collider.GetComponent<NPC>();
			BattleManager.GetInstance ().PlayerEnterNpc (ID, npc.ID);
		} else if (tag == "DropItem") {
			DropItem dropItem = collider.GetComponent<DropItem>();
			dropItem.OnHit();
			BattleManager.GetInstance ().PlayerTakeItem (ID, dropItem.ID);
		}
	}  
	void OnTriggerExit(Collider collider)  {  
		//Debug.Log("OnTriggerExit");  
	}
}
