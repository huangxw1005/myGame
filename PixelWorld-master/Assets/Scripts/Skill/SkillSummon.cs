using UnityEngine;
using System.Collections;

public class SkillSummonInfo {
	public int id;
	public int skillid;
	public Character target;
}

/// <summary>
/// Skill summon.
/// 技能召唤物实体（可以是火球、陷阱等）
/// </summary>
public class SkillSummon : MonoBehaviour
{
	public int ID {get; set;}
	private int templeteID;

	private CfgSkillSummon cfg;

	private Character owner;
	private Character target;

	public SkillEffect[] skilleffects;

	private Skill skill;

	private float timer;
	private int hitcount=0;	// 统计攻击次数

	public void SetInfo(int id, Character actor, SkillSummonInfo info) {
		ID = id;
		owner = actor;

		cfg  = CfgManager.GetInstance().SkillSummons[info.id];

		skill = actor.GetSkill(info.skillid);

		// skill effects
		int count = cfg.skilleffects.Length;
		if (count > 0) {
			skilleffects = new SkillEffect[count];
			for (int i = 0; i < count; i ++) {
				skilleffects[i] = SkillEffect.CreateSkillEffect(cfg.skilleffects[i], skill);
			}
		}

	}

	void Start() {
		
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > cfg.life) {
			End();
		}

		if (cfg.speed > 0) {
			transform.position += transform.forward * cfg.speed * Time.deltaTime;
		}
	}

	// 更新方向
	void UpdateDirection() {
		
	}

	// 攻击
	void OnAttack(Character actor) {

		if (actor.LastSummonID == ID) return;

		actor.LastSummonID = ID;
		actor.ActHit();
		if (actor is Enemy) {
			BattleManager.GetInstance().EnemyHit(actor.ID, owner.ID);
		} else {
			BattleManager.GetInstance().PlayerHit(actor.ID, owner.ID);
		}
		actor.AttackEffect(cfg.hiteffect, 1, new Vector3(0, 0.5f, 0));

		for (int i = 0; i < skilleffects.Length; i ++) {
			SkillEffect effect = skilleffects[i];
			if (effect.CheckTrigger(SkillEffectTrigger.SKILL_EFFECT_TRIGGER_HIT)) {
				effect.DoEffect(owner, actor);
			}
		}

		hitcount ++;

		// limit attack count
		if (cfg.hitmax != 0 && hitcount >= cfg.hitmax) {
			End();
		}
	}

	// 攻击结束（用于范围summon）
	void OnAttackEnd(Character actor) {
	}

	void End() {
		Debug.Log("SkillSummon:End");
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider collider) {
		string tag = collider.gameObject.tag;
		Debug.Log("SkillSummon.OnTriggerEnter " + tag);  
		if (tag == "Enemy") {
			if (skill.TargetType == SkillTarget.SKILL_TARGET_ENEMY) {
				Character actor = collider.GetComponent<Character>();
				OnAttack(actor);
			}
		} else if (tag == "Player") {
			if (skill.TargetType == SkillTarget.SKILL_TARGET_TEAM) {
				Character actor = collider.GetComponent<Character>();
				OnAttack(actor);
			}
		}
	}

	void OnTriggerExit(Collider collider) {
		string tag = collider.gameObject.tag;
		Debug.Log("SkillSummon.OnTriggerExit " + tag);  
		if (tag == "Enemy") {
			if (skill.TargetType == SkillTarget.SKILL_TARGET_ENEMY) {
				Character actor = collider.GetComponent<Character>();
				OnAttackEnd(actor);
			}
		} else if (tag == "Player") {
			if (skill.TargetType == SkillTarget.SKILL_TARGET_TEAM) {
				Character actor = collider.GetComponent<Character>();
				OnAttackEnd(actor);
			}
		}
	}

}