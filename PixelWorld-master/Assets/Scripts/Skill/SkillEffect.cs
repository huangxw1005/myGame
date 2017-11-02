using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Skill effect.
/// </summary>
public class SkillEffect
{
	public CfgSkillEffect cfg;

	private Skill ownSkill;

	public static SkillEffect CreateSkillEffect(int id, Skill skill) {
		if (CfgManager.GetInstance().SkillEffects.ContainsKey(id)) {
			return new SkillEffect(id, skill);
		}
		return null;
	}

	public SkillEffect(int id, Skill skill) {
		if (CfgManager.GetInstance().SkillEffects.ContainsKey(id) == false) {
			Debug.LogErrorFormat("SkillEffect cfg not found: {0}", id);
			return;
		}
		ownSkill = skill;
		cfg = CfgManager.GetInstance().SkillEffects[id];
	}

	public bool CheckTrigger(SkillEffectTrigger trigger) {
		return cfg.trigger == trigger;
	}

	/// <summary>
	/// Dos the effect.
	/// 默认hit触发的buff 都是加到被攻击者身上的
	/// </summary>
	/// <param name="owner">Owner.</param>
	/// <param name="target">Target.</param>
	public void DoEffect(Character owner, Character target=null) {
		Debug.LogFormat("DoEffect {0} {1} {2}", cfg.operation, cfg.id, cfg.name);
		switch (cfg.operation) {
		case SkillEffectOperation.OPERTION_NONE:
			break;
		case SkillEffectOperation.OPERTION_HP:
			DoHP(owner);
			break;
		case SkillEffectOperation.OPERTION_SP:
			DoSP(owner);
			break;
		case SkillEffectOperation.OPERTION_ADDBUFF:
			if (target != null) DoAddBuff(target);
			else DoAddBuff(owner);
			break;
		case SkillEffectOperation.OPERTION_ADDBUFF_GROUP:
			if (target != null) DoAddBuffGroup(target);
			else DoAddBuffGroup(owner);
			break;
		case SkillEffectOperation.OPERTION_SUMMON:
			DoSummon(owner);
			break;
		case SkillEffectOperation.OPERTION_SUMMON_TARGET:
			DoSummonTarget(owner);
			break;
		case SkillEffectOperation.OPERTION_SUMMON_TARGET_POS:
			DoSummonTargetPos(owner);
			break;
		}
	}

	void DoHP(Character actor) {
		Debug.Log("DoHP");
		int[] args = cfg.args;
		int value = 0;
		if (args[0] == 0) {	// 固定值
			value = args[1];
		} else {// 百分比
			value = args[1]*actor.HPMax;
		}

		actor.AddHP(value);
		BattleManager.GetInstance().ActorAddHP(actor, value);
	}

	void DoSP(Character actor) {
		Debug.Log("DoSP");
		int[] args = cfg.args;
		int value = 0;
		if (args[0] == 0) {	// 固定值
			value = args[1];
		} else {// 百分比
			value = args[1]*actor.MPMax;
		}
		actor.AddMP(value);
		BattleManager.GetInstance().ActorAddSP(actor, value);
	}

	void DoAddBuff(Character owner) {
		Debug.Log("DoAddBuff");
		int id = cfg.args[0];
		owner.AddBuff(id);
	}

	void DoAddBuffGroup(Character owner) {
		Debug.Log("DoAddBuffGroup");
		int id = cfg.args[0];
	}

	static int s_SummonUID = 0;
	void DoSummon(Character actor) {
		Debug.Log("DoSummon");
		s_SummonUID ++;

		int id = cfg.args[0];
		int angle = cfg.args[1];
		int count = cfg.args[2];

		float angleStart = actor.transform.rotation.eulerAngles.y;


		SkillSummonInfo info = new SkillSummonInfo();
		info.id = id;
		info.skillid = ownSkill.ID;

		int angleHalf = angle/2;

		if (count % 2 == 0) { // 偶数	
			// border
			actor.AddSummon(s_SummonUID,  actor.transform.position, Quaternion.Euler(new Vector3(0, angleStart-angleHalf, 0)), info);
			actor.AddSummon(s_SummonUID,  actor.transform.position, Quaternion.Euler(new Vector3(0, angleStart+angleHalf, 0)), info);

			int left = count - 2;
			if (left > 0) {
				int anglePer = angleHalf / (left+1);
				angleStart -= angleHalf;
				for (int i = 0; i < left; i ++) {
					int angleOffset = anglePer*(i+1);
					actor.AddSummon(s_SummonUID,  actor.transform.position, Quaternion.Euler(new Vector3(0, angleStart+angleOffset, 0)), info);
			 	}
			}
		} else { // 奇数		
			// middle
			actor.AddSummon(s_SummonUID,  actor.transform.position, Quaternion.Euler(new Vector3(0, angleStart, 0)), info);

			if (count > 1) {// 3, 5...
				// border
				actor.AddSummon(s_SummonUID,  actor.transform.position, Quaternion.Euler(new Vector3(0, angleStart-angleHalf, 0)), info);
				actor.AddSummon(s_SummonUID,  actor.transform.position, Quaternion.Euler(new Vector3(0, angleStart+angleHalf, 0)), info);

				int left = (count - 3)/2;
				if (left > 0) {
					int anglePer = angleHalf / (left+1);
				 	for (int i = 0; i < left; i ++) {
						int angleOffset = anglePer*(i+1);
						actor.AddSummon(s_SummonUID,  actor.transform.position, Quaternion.Euler(new Vector3(0, angleStart-angleOffset, 0)), info);
						actor.AddSummon(s_SummonUID,  actor.transform.position, Quaternion.Euler(new Vector3(0, angleStart+angleOffset, 0)), info);
				 	}
				}
			}
		}

	}

	// 选中目标朝向召唤(必有目标，才能释放技能)
	void DoSummonTarget(Character actor) {
		Debug.Log("DoSummonTarget");
		s_SummonUID ++;

		// TO-DO
	}

	// 选中目标位置召唤(必有目标，才能释放技能)
	void DoSummonTargetPos(Character actor) {
		Debug.Log("DoSummonTarget");
		s_SummonUID ++;

		int id = cfg.args[0];
		int angle = cfg.args[1];
		int count = cfg.args[2];

		float angleStart = actor.transform.rotation.eulerAngles.y;

		List<Character> lstActor;
		if (angle == 360) {
			// circle
			lstActor = CharacterManager.Instance.FindActor(actor.transform.position, SkillTarget.SKILL_TARGET_ENEMY, SkillRegion.SKILL_REGION_CIRCLE, ownSkill.Aoe, angle, Vector3.zero);
		} else {
			lstActor = CharacterManager.Instance.FindActor(actor.transform.position, SkillTarget.SKILL_TARGET_ENEMY, SkillRegion.SKILL_REGION_SECTOR, ownSkill.Aoe, angle, actor.transform.forward);
		}

		lstActor = CharacterManager.Instance.SortActor(actor.transform.position, lstActor);

		int i = 0;
		for (; i < count && i < lstActor.Count; i++ ) {
			Character target = lstActor[i];

			Vector3 dir = target.transform.position - actor.transform.position;
			dir.y = 0.0f;
			dir.Normalize();

			SkillSummonInfo info = new SkillSummonInfo();
			info.id = id;
			info.skillid = ownSkill.ID;
			info.target = target;

			actor.AddSummon(s_SummonUID,  target.transform.position, Quaternion.identity, info);
		} 
	}
}