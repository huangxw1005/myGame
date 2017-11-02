using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

	public float JumpHeight = 4;
	//重力
	public float Gravity=10;

	//
	private float yMove = 0;	// 垂直速度
	private Vector3 move = Vector3.zero; 

	private int m_AttackIdx = 0;

	//角色控制器
	private CharacterController m_Controller;

	//角色控制器
	private Animator m_Animator;

	private Player m_Player;

	private bool hasAdjustRotation;

	void Start ()
	{
		//获取角色控制器
		m_Controller=GetComponent<CharacterController>();
		m_Animator = GetComponentInChildren<Animator>();
		//
		m_Player=GetComponentInChildren<Player>();
	}

	void Update () {
		//只有处于正常状态时玩家可以行动
		MoveManager ();

	}
	 
	//移动管理
	void MoveManager()
	{
		if (!m_Player.IsControllable)
			return;
		
		//移动方向
		Vector3 mDir=Vector3.zero;

		// Read input
		float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
		float vertical = CrossPlatformInputManager.GetAxis("Vertical");


		bool bJump = CrossPlatformInputManager.GetButtonUp ("Jump");
		bool bAttack = CrossPlatformInputManager.GetButtonUp ("Fire1");
		bool [] bSkills = new bool[3];
		bSkills[0] = CrossPlatformInputManager.GetButtonUp ("Fire2");
		bSkills[1] = CrossPlatformInputManager.GetButtonUp ("Fire3");
		bSkills[2] = CrossPlatformInputManager.GetButtonUp ("Fire4");
		if (bJump) {
			Debug.LogFormat("bJump {0}", bJump);
		}
		bool isMoving = false;

		if (Mathf.Abs (horizontal) > 0.01f || Mathf.Abs (vertical) > 0.01f) {
			// move
			isMoving = true;
			mDir.x = horizontal;
			mDir.z = vertical;	

			// normalize input if it exceeds 1 in combined length:
			if (mDir.sqrMagnitude > 1) {
				mDir.Normalize();
			}

			// camera 
			mDir = Camera.main.transform.forward * mDir.z + Camera.main.transform.right * mDir.x;
			mDir.y = 0;
		} else {
			// move stop
			isMoving = false;
		}

		if (m_Controller.isGrounded == false) {
			bJump = false;
			bAttack = false;
			for (int i = 0; i < bSkills.Length; i ++) bSkills[i] = false;
		}

		for (int i = 0; i < bSkills.Length; i ++) {
			if (bSkills[i] == false) continue;
			Skill skill = m_Player.GetSkillByIdx(i);
			if (skill != null ) {
				if (skill.IsColdDown) {
					bSkills[i] = false;
					BattleManager.GetInstance().ShowTip("BATTLE_SKILL_COLDDOWN");
				} else  if( !skill.CheckSP(m_Player)) {
					bSkills[i] = false;
					BattleManager.GetInstance().ShowTip("BATTLE_MP_NOT_ENOUGH");
				}
			} else {
				bSkills[i] = false;
				Debug.LogWarning("skill not found");
			}
		}

		if (bJump) {
			yMove = JumpHeight;
			Debug.LogFormat("bJump {0}", bJump);
		}

		m_Animator.SetBool("isMoving", isMoving);
		//if (m_Animator.GetBool("isGrounded") != m_Controller.isGrounded) {
			m_Animator.SetBool("isGrounded", m_Controller.isGrounded);
		//}

		AnimatorStateInfo cur = m_Animator.GetCurrentAnimatorStateInfo(0);
		if (m_Animator.IsInTransition(0)) {
			// 融合时
			AnimatorStateInfo next = m_Animator.GetNextAnimatorStateInfo(0);
		} else {
			// 不融合时
			if (cur.IsName("run") && isMoving) {
				transform.forward = mDir;
				move.x = mDir.x * m_Player.Speed;
				move.z = mDir.z * m_Player.Speed;
				m_AttackIdx = 0;
				m_Animator.SetBool("bJump", bJump);
				if (bAttack) {
					m_AttackIdx = 1;
				}
				m_Animator.SetInteger("AttackIdx", m_AttackIdx);
				for (int i = 0; i < bSkills.Length; i ++) {
					m_Animator.SetBool("bSkill"+(i+1), bSkills[i]);
				}
				m_Animator.SetBool("bHit", false);
				//Debug.Log("run");
			} else if (cur.IsName("idle")) {
				hasAdjustRotation = false;
				move.x = 0;
				move.z = 0;
				m_Animator.SetBool("bJump", bJump);
				m_AttackIdx = 0;
				if (bAttack) {
					m_AttackIdx = 1;
				}
				m_Animator.SetInteger("AttackIdx", m_AttackIdx);
				for (int i = 0; i < bSkills.Length; i ++) {
					m_Animator.SetBool("bSkill"+(i+1), bSkills[i]);
				}
				//Debug.Log("idle");
			} else if (cur.IsName("jump")) {
				if (yMove > 0 && cur.normalizedTime < 0.5f) {	// 开始jump
					move *= 0.8f;
					move.y = yMove;		// 向上初速度
					yMove = 0;
					m_Animator.SetBool("isGrounded", false);
				}
				Debug.Log("jump " + cur.normalizedTime);
			} else if (cur.IsName("attack1_1")) {
				if (hasAdjustRotation == false) {
					m_Player.AutoRotateToEnemy();
					hasAdjustRotation = true;
				} else {
					// allow rotate while attack
					if (isMoving)  transform.forward = mDir;
				}
				move.x = 0;
				move.z = 0;
				if (bAttack && m_AttackIdx == 1) {
					m_AttackIdx ++;
					m_Animator.SetInteger("AttackIdx", m_AttackIdx);
				}
			} else if (cur.IsName("attack1_2")) {
				if (isMoving)  transform.forward = mDir;
				move.x = 0;
				move.z = 0;
				if (bAttack && m_AttackIdx == 2) {
					m_AttackIdx ++;
					m_Animator.SetInteger("AttackIdx", m_AttackIdx);
				}
			} else if (cur.IsName("attack1_3")) {
				if (isMoving)  transform.forward = mDir;
				move.x = 0;
				move.z = 0;
			} else if (cur.IsName("hit")) {
				move.x = 0;
				move.z = 0;
				m_Animator.SetBool("bHit", false);
			} else if (cur.IsName("skill1")) {
				if (hasAdjustRotation == false) {
					m_Player.AutoRotateToEnemy();
					hasAdjustRotation = true;
					StartCoroutine(CastSkill(0));
					BattleManager.GetInstance().CastSkill(0);
				}
				move.x = 0;
				move.z = 0;
				m_Animator.SetBool("bSkill1", false);
			} else if (cur.IsName("skill2")) {
				if (hasAdjustRotation == false) {
					m_Player.AutoRotateToEnemy();
					hasAdjustRotation = true;
					StartCoroutine(CastSkill(1));
					BattleManager.GetInstance().CastSkill(1);
				}
				move.x = 0;
				move.z = 0;
				m_Animator.SetBool("bSkill2", false);
			} else if (cur.IsName("skill3")) {
				if (hasAdjustRotation == false) {
					m_Player.AutoRotateToEnemy();
					hasAdjustRotation = true;
					StartCoroutine(CastSkill(2));
					BattleManager.GetInstance().CastSkill(2);
				}
				move.x = 0;
				move.z = 0;
				m_Animator.SetBool("bSkill3", false);
			}
		}

		if (!m_Controller.isGrounded) {
			// 重力下降
			move.y -= Gravity *Time.deltaTime;
		}

		CollisionFlags flags = m_Controller.Move(move * Time.deltaTime);
	}


	IEnumerator CastSkill(int idx) {
		Skill skill = m_Player.CastSkill(idx);
		if (skill != null) {
			m_Player.AddMP(-skill.SPCost);
			skill.IsColdDown = true;
			yield return new WaitForSeconds(skill.ColdDown);
			skill.IsColdDown = false;
		}
	}
}