using UnityEngine;
using System.Collections;

/// <summary>
/// Skill.
/// 技能实体，
/// </summary>
public class Skill
{
	private int id;
	private string name;
	private string desc;
	private string animation;
	private string sound;
	private string hiteffect;
	private int[] hitpoints;
	private int spcost;
	private float colddown;
	private SkillTarget targettype;
	private int aoe;

	public SkillEffect[] skilleffects;

	public int ID {get {return id;}}
	public string AnimationName {get {return animation;}}
	public int[] HitPoints {get {return hitpoints;}}
	public int Aoe {get {return aoe;}}
	public SkillTarget TargetType {get {return targettype;}}
	public int SPCost {get {return spcost;}}
	public float ColdDown {get {return colddown;}}

	public bool IsColdDown {get; set;}

	public static Skill CreateSkill(int id) {
		if (CfgManager.GetInstance().Skills.ContainsKey(id)) {
			return new Skill(id);
		}
		return null;
	}


	public Skill(int id) {
		if (CfgManager.GetInstance().Skills.ContainsKey(id) == false) {
			Debug.LogErrorFormat("skill cfg not found: {0}", id);
			return;
		}

		CfgSkill cfg = CfgManager.GetInstance().Skills[id];
		this.id = cfg.id;
		name = cfg.name;
		desc = cfg.desc;
		animation = cfg.animation;
		sound = cfg.sound;
		hiteffect = cfg.hiteffect;
		hitpoints = cfg.hitpoints;
		targettype = cfg.targettype;
		aoe = cfg.aoe;
		spcost = cfg.spcost;
		colddown = cfg.colddown;

		// skill effects
		int count = cfg.skilleffects.Length;
		if (count > 0) {
			skilleffects = new SkillEffect[count];
			for (int i = 0; i < count; i ++) {
				skilleffects[i] = SkillEffect.CreateSkillEffect(cfg.skilleffects[i], this);
			}
		}

	}

	public bool CheckSP(Character actor) {
		return actor.MP >= spcost;
	}

}