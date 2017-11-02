using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillState {
	public Character actor;
	public Skill skill;
	public float time=0;
}

public class SkillStateManager : Singleton<SkillStateManager>
{

	List<SkillState> lstStates = new List<SkillState>();

	void Start() {
	}

	void Update() {
		for (int i = lstStates.Count-1; i >=0 ; i --) {
			SkillState state = lstStates[i];
			if (state.actor.IsAlive() == false) {
				lstStates.RemoveAt(i);
				continue;
			}

			Skill skill = state.skill;
			AnimatorStateInfo cur = state.actor.Animator.GetCurrentAnimatorStateInfo(0);
			if (cur.IsName(skill.AnimationName)) {	// 播放中
				float time = cur.normalizedTime;

				// check all effects
				for (int j = 0; j < skill.HitPoints.Length; j ++) {
					float t = skill.HitPoints[j] * 0.01f;
					if (t <= time && t > state.time) {
						for (int k = 0; k < skill.skilleffects.Length; k++) {
							SkillEffect effect = skill.skilleffects[k];
							if (effect.CheckTrigger(SkillEffectTrigger.SKILL_EFFECT_TRIGGER_HITPOINT)) {
								effect.DoEffect(state.actor);
							}
						}
					}
				}
				state.time = time;
			} else { // 播放结束
				if (state.time > 0.5f) {
					for (int j = 0; j < skill.skilleffects.Length; j ++) {
						SkillEffect effect = skill.skilleffects[j];
						if (effect.CheckTrigger(SkillEffectTrigger.SKILL_EFFECT_TRIGGER_END)) {
							effect.DoEffect(state.actor);
						}
					}
					lstStates.RemoveAt(i);
				}
			}
		}
	}

	public void AddSkillState(Character actor, Skill skill) {
		SkillState state = new SkillState();
		state.actor = actor;
		state.skill = skill;

		// check start
		for (int i = 0; i < skill.skilleffects.Length; i ++) {
			SkillEffect effect = skill.skilleffects[i];
			if (effect.CheckTrigger(SkillEffectTrigger.SKILL_EFFECT_TRIGGER_START)) {
				effect.DoEffect(actor);
			}
		}

		lstStates.Add(state);
	}
}