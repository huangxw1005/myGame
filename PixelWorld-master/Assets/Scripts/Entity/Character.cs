using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CharaterState {
	IDLE=0,
	IDLE2,
	RUN,
	JUMP,
	ATTACK,
	ATTACK1_1,
	ATTACK1_2,
	ATTACK1_3,
	ATTACK2,
	ATTACK3,
	HIT,
	DEATH,
}

public class Character : MonoBehaviour {

	public int ID {get; set;}			// uid
	public int ModelID {get; set;}		// model id
	public string Name {get; set;}		// name

	// attrs
	public int HP = 100;				// hp
	public int HPMax = 100;			// hp max
	public int MP = 100;				// mp
	public int MPMax = 100;			// mp max
	
	public float Speed = 3.0f;			// 移动速度
	public float DistSight = 5.0f;		// 可视范围
	public float DistAttack = 1.0f; 		// 攻击范围

	public bool IsControllable {get; set;}		// is contollable?

	public bool IsAlive() { return HP > 0; }

	public bool IsUser { get; set;}			// 是否是玩家自己

	public static string[] PropertyName = new string[] {
		"HP",
		"HPMax",
		"SP",
		"SPMax",
		"Atk",
		"Def",
		"Hit",			// 命中
		"Dodge",			// 闪避
		"Crit",			// 暴击

		"MoveSpeed",		// 移动速度		
		"AttackSpeed",		// 攻击速度


		// for buff
		"DamageReduce",	// 伤害减免
	};
	public Hashtable Property 				= null;
	public Hashtable PropertyBuf 				= null;

	// skills
	public List<int> lstSkills 					= new List<int>();
	public Dictionary<int, Skill> dicSkills 			= new Dictionary<int, Skill>();
	public int LastSummonID {get; set;}		// last summon hit id

	// buffs
	public List<Buff> buffs 					= new List<Buff>();
	private float HPRecoverTimer 				= 0;
	private float MPRecoverTimer 				= 0;
	public bool IsStun {get; set;}
	public bool IsFrozen {get; set;}


	//动画组件
	protected Animator m_Animator;
	public Animator Animator {get{return m_Animator;}}

	protected CharaterState m_CharacterState;
	public CharaterState CharcterState {get{return m_CharacterState;}}

	//
	protected CharacterController m_CharacterController;

	protected GameObject AttackBox;

	// prop
	public void AddHP(int value) {
		HP += value;
		HP = HP > HPMax ? HPMax : HP;

		if (IsUser) BattleManager.GetInstance().HPChange(HP, HPMax);
	}
	public void AddMP(int value) {
		MP += value;
		MP = MP > MPMax ? MPMax : MP;

		if (IsUser) BattleManager.GetInstance().MPChange(MP, MPMax);
	}

	protected virtual void Awake() {
		m_CharacterController = GetComponent<CharacterController>();
		m_Animator = GetComponentInChildren<Animator>();

		AttackBox = transform.Find("AttackBox").gameObject;
		AttackBox.SetActive(false);

		IsControllable = true;
	}

	protected virtual void Start() {
	}
	
	// Update is called once per frame
	void Update () {
		bool bChanged = false;
		for (int i = buffs.Count-1; i >= 0; i --) {
			if (buffs[i].IsDead()) {
				buffs.RemoveAt(i);
				bChanged = true;
			} else {
				buffs[i].Update();
			}
		}
		if (bChanged) {
			RefreshProperty();
		}

		if (HP < HPMax) {
			HPRecoverTimer += Time.deltaTime;
			if (HPRecoverTimer > 1.0f) {
				AddHP(1);
				HPRecoverTimer = 0f;
			}
		}
		if (MP < MPMax) {
			MPRecoverTimer += Time.deltaTime;
			if (MPRecoverTimer > 1.0f) {
				AddMP(1);
				MPRecoverTimer = 0f;
			}
		}
	}

	public float GetRadius() {
		return m_CharacterController.radius;
	}

	public virtual void ActIdle() {
		m_CharacterState = CharaterState.IDLE;
		m_Animator.CrossFade ("idle", 0);
	}
	public virtual void ActRun() {
		m_CharacterState = CharaterState.RUN;
		m_Animator.CrossFade ("run", 0);
	}
	public virtual void ActAttack() {
		m_CharacterState = CharaterState.ATTACK;
		m_Animator.CrossFade ("attack", 0);
	}

	public virtual void ActHit() {
		m_CharacterState = CharaterState.HIT;
		m_Animator.SetBool("bHit", true);
		StartCoroutine(ResetValue("bHit"));
	}
	public virtual void ActDie() {
		IsControllable = false;
		m_CharacterState = CharaterState.DEATH;
		m_Animator.SetBool("bDie", true);
		AttackBox.SetActive(false);
	}

	public IEnumerator ResetValue(string name)
	{
		yield return null;
		m_Animator.SetBool(name, false);
	}

	protected virtual void StartAttack() {
	}

	public void OnEventAttack(string param) {
		//Debug.LogFormat("OnEventAttack {0} {1}", ID, param);
		if (param == "start") {
			AttackBox.SetActive(true);

			StartAttack();
		} else {
			AttackBox.SetActive(false);
		}
	}

	public Skill AddSkill(int skillid) {
		if (dicSkills.ContainsKey(skillid)) {
			return dicSkills[skillid];
		} else {
			Skill skill = Skill.CreateSkill(skillid);
			if (skill != null) {
				dicSkills.Add(skillid, skill);
				lstSkills.Add(skillid);
			} else {
				Debug.LogWarningFormat("skill {0} not exist", skillid);
			}
			return null;
		}
	}
	public Skill GetSkill(int skillid) {
		if (dicSkills.ContainsKey(skillid)) {
			return dicSkills[skillid];
		} else {
			Debug.LogWarningFormat("skill {0} not exist", skillid);
			return null;
		}
	}
	public Skill GetSkillByIdx(int idx) {
		if (idx < 0 || idx >= lstSkills.Count) {
			return null;
		} else {
			int id = lstSkills[idx];
			return dicSkills[id];
		}
	}

	public Skill CastSkill(int idx) {
		Debug.LogFormat("CastSkill {0}", idx);

		if (idx < 0 || idx >= lstSkills.Count) {
			return null;
		}
		int skillid = lstSkills[idx];
		if (dicSkills.ContainsKey(skillid)) {
			Skill skill = dicSkills[skillid];
			SkillStateManager.Instance.AddSkillState(this, skill);
			return skill;
		} else {
			Debug.LogWarningFormat("skill {0} not exist", skillid);
		}
		return null;
	}

	public void AddSummon(int uid, Vector3 pos, Quaternion rot, SkillSummonInfo info) {
		if (CfgManager.GetInstance().SkillSummons.ContainsKey(info.id)) {
			CfgSkillSummon cfg = CfgManager.GetInstance().SkillSummons[info.id];
			GameObject prefab = (GameObject)ResourceManager.Instance.LoadAsset(cfg.prefab);
			GameObject go = Instantiate(prefab) as GameObject;
			go.tag = gameObject.tag + "Missile";
			go.transform.localScale = Vector3.one;
			go.transform.rotation = rot;
			if (cfg.speed > 0) {//移动
				go.transform.position = pos + new Vector3(0, 0.5f, 0) + transform.forward * 0.5f;
			} else {
				go.transform.position = pos ;
			}
			SkillSummon summon = go.AddComponent<SkillSummon>();
			summon.SetInfo(uid, this, info);
		}
	}

	public GameObject AttackEffect(string path,  float life, Vector3 pos) {
		GameObject prefab = (GameObject)ResourceManager.Instance.LoadAsset(path);
		GameObject go = Instantiate(prefab) as GameObject;
		go.transform.SetParent(transform);
		go.transform.localScale = Vector3.one;
		go.transform.localPosition = pos;
		Destroy(go, life);
		return go;
	}

	public void AddBuff(int id) {
		// check buff states
		// TO-DO

		Buff buff = Buff.CreateBuff(id);
		buff.SetOwner(this);
		buff.Start();

		buffs.Add(buff);
	}

	private void RefreshProperty() {
		
	}

	protected IEnumerator HitBack(Vector3 forward) {
		IsControllable = false;

		Object prefab = ResourceManager.Instance.LoadAsset("Prefabs/HitBack");
		GameObject go = GameObject.Instantiate(prefab) as GameObject;
		go.transform.localScale = Vector3.one;
		go.transform.localPosition = transform.localPosition;
		go.transform.forward = forward;

		UpdateTransformFromAnimation script = go.GetComponent<UpdateTransformFromAnimation>();
		script.target = this;

		yield return null;

		Debug.Log (script.bComplete);

		while (script.bComplete) {
			yield return null;
		}
		
		Debug.Log (script.bComplete);

		Destroy (go);

		IsControllable = true;
	}

	protected IEnumerator HitFly(Vector3 forward) {
		IsControllable = false;

		Object prefab = ResourceManager.Instance.LoadAsset("Prefabs/HitFly");
		GameObject go = GameObject.Instantiate(prefab) as GameObject;
		go.transform.localScale = Vector3.one;
		go.transform.localPosition = transform.localPosition;
		go.transform.forward = forward;

		UpdateTransformFromAnimation script = go.GetComponent<UpdateTransformFromAnimation>();
		script.target = this;

		yield return null;

		Debug.Log (script.bComplete);

		while (script.bComplete) {
			yield return null;
		}

		Destroy (go);

		if (HP <= 0) m_CharacterController.enabled = false;

		IsControllable = true;
	}
}
