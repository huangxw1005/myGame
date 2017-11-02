/// <summary>
/// CfgManager
/// 管理所有配置
/// </summary>
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;


public struct CfgMonster {
	public int id;
	public string name;
	public string prefab;
	public string bornSound;
	public string deathSound;
}

public struct CfgSkill {
	public int id;
	public string name;
	public string desc;
	public string animation;
	public string effect;
	public string sound;
	public int[] skilleffects;
	public int[] hitpoints;
	public string hiteffect;
	public int spcost;				// 魔法消耗
	public float colddown;			// 冷却时间
	public SkillTarget targettype;		// 施法对象
	public byte aoe;			// aoe范围
	public byte aoetype;			// aoe形状 0:圆形 1:扇形
	public byte aoeangle;		// 扇形时角度
}

public enum SkillTarget {
	SKILL_TARGET_SELF=0,		//  自己
	SKILL_TARGET_TEAM,			//  友方
	SKILL_TARGET_ENEMY,		//  敌方
}

public enum SkillRegion {
	SKILL_REGION_CIRCLE=0,		//  圆形
	SKILL_REGION_SECTOR,		//  扇形
}

public enum SkillEffectTrigger {
	SKILL_EFFECT_TRIGGER_START=0,		// 播放开始时
	SKILL_EFFECT_TRIGGER_HITPOINT,		// 打击点
	SKILL_EFFECT_TRIGGER_HIT,			// 击中时
	SKILL_EFFECT_TRIGGER_END,			// 播放结束时
}

public enum SkillEffectOperation {
	OPERTION_NONE=0,		
	OPERTION_HP,					// add/reduce hp self
	OPERTION_SP,					// add/reduce sp self
	OPERTION_ADDBUFF,				// add buff one
	OPERTION_ADDBUFF_GROUP,		// add buff group
	OPERTION_SUMMON,				// 召唤-当前朝向		
	OPERTION_SUMMON_TARGET,		// 召唤-选择目标朝向
	OPERTION_SUMMON_TARGET_POS,	// 召唤-目标位置
}
public struct CfgSkillEffect {
	public int id;
	public string name;
	public SkillEffectTrigger trigger;
	public SkillEffectOperation operation;
	public int[] args;
}
public struct CfgSkillSummon {
	public int id;
	public string name;
	public string prefab;
	public int speed;
	public float life;
	public int hitmax;
	public string hiteffect;
	public int[] skilleffects;
}
public struct CfgBuff {
	public int id;
	public string name;
	public string prefab;
	public float life;
	public BuffOperation operation;
	public int[] args;
}
public enum BuffOperation {
	OPERATION_NONE=0,		
	OPERATION_HP,					// add/reduce hp self
	OPERATION_SP,					// add/reduce sp self
	OPERATION_DAMAGE,				// 持续伤害

	OPERATION_ADD,					// add property [speed, attack... ]
	OPERATION_REDUCE,				// reduce property

	OPERATION_STUN,					//  stun target
	OPERATION_FROZEN,				// frozen target
}

public class CfgManager {
	private static CfgManager _instance;
	public static CfgManager GetInstance() {
		if (_instance == null) _instance = new CfgManager();
		return _instance;
	}

	public void Clear() {
		_instance = null;
	}
		
	// 地图配置
	private Dictionary<int, CfgMonster> m_Monsters = new Dictionary<int, CfgMonster>();
	public Dictionary<int, CfgMonster> Monsters {get {return m_Monsters;}}

	// skill
	private Dictionary<int, CfgSkill> m_Skills = new Dictionary<int, CfgSkill>();
	public Dictionary<int, CfgSkill> Skills {get {return m_Skills;}}

	// skill effect
	private Dictionary<int, CfgSkillEffect> m_SkillEffects = new Dictionary<int, CfgSkillEffect>();
	public Dictionary<int, CfgSkillEffect> SkillEffects {get {return m_SkillEffects;}}

	// skill summon
	private Dictionary<int, CfgSkillSummon> m_SkillSummons = new Dictionary<int, CfgSkillSummon>();
	public Dictionary<int, CfgSkillSummon> SkillSummons {get {return m_SkillSummons;}}

	// buff
	private Dictionary<int, CfgBuff> m_Buffs = new Dictionary<int, CfgBuff>();
	public Dictionary<int, CfgBuff> Buffs {get {return m_Buffs;}}


	public void Init() {

		// monster
		TextAsset asset = ResourceManager.Instance.LoadAsset("cfg/monster") as TextAsset;
		JsonData data = JsonMapper.ToObject(asset.text);
		foreach (string key in data.Keys) {
			JsonData value = data [key];
			CfgMonster cfg = new CfgMonster();
			cfg.id = int.Parse (key);
			cfg.name = (string)value["name"];
			cfg.prefab = (string)value["prefab"];
			m_Monsters.Add(cfg.id, cfg);
		}

		// skill
		asset = ResourceManager.Instance.LoadAsset("cfg/skill") as TextAsset;
		data = JsonMapper.ToObject(asset.text);
		foreach (string key in data.Keys) {
			JsonData value = data [key];
			CfgSkill cfg = new CfgSkill();
			cfg.id = int.Parse (key);
			cfg.name = (string)value["name"];
			cfg.desc =  (string)value["desc"];
			cfg.animation =  (string)value["animation"];
			cfg.effect =  (string)value["effect"];
			cfg.sound =  (string)value["sound"];
			cfg.hiteffect =  (string)value["hiteffect"];
			cfg.spcost = (int)value["spcost"];
			cfg.colddown = (int)value["colddown"]/1000f;
			cfg.targettype =  (SkillTarget)(int)value["targettype"];
			cfg.aoe = (byte)value["aoe"];
			JsonData effects = value["skilleffect"];
			cfg.skilleffects = new int[effects.Count];
			for (int i = 0; i < effects.Count; i ++) {
				cfg.skilleffects[i] = (int)effects[i];
			}
			JsonData points = value["hitpoint"];
			cfg.hitpoints = new int[points.Count];
			for (int i = 0; i < points.Count; i ++) {
				cfg.hitpoints[i] = (int)points[i];
			}

			m_Skills.Add(cfg.id, cfg);
		}

		// skill effect
		asset = ResourceManager.Instance.LoadAsset("cfg/skilleffect") as TextAsset;
		data = JsonMapper.ToObject(asset.text);
		foreach (string key in data.Keys) {
			JsonData value = data [key];
			CfgSkillEffect cfg = new CfgSkillEffect();
			cfg.id = int.Parse (key);
			cfg.name = (string)value["name"];
			cfg.trigger =  (SkillEffectTrigger)(int)value["trigger"];
			cfg.operation =  (SkillEffectOperation)(int)value["operation"];
			JsonData args = value["args"];
			cfg.args = new int[args.Count];
			for (int i = 0; i < args.Count; i ++) {
				cfg.args[i] = (int)args[i];
			}
			m_SkillEffects.Add(cfg.id, cfg);
		}

		// skill summon
		asset = ResourceManager.Instance.LoadAsset("cfg/skillsummon") as TextAsset;
		data = JsonMapper.ToObject(asset.text);
		foreach (string key in data.Keys) {
			JsonData value = data [key];
			CfgSkillSummon cfg = new CfgSkillSummon();
			cfg.id = int.Parse (key);
			cfg.name = (string)value["name"];
			cfg.prefab = (string)value["prefab"];
			cfg.speed = (int)value["speed"];
			cfg.life = (int)value["life"]/1000f;
			cfg.hitmax = (int)value["hitmax"];
			cfg.hiteffect =  (string)value["hiteffect"];
			JsonData effects = value["skilleffect"];
			cfg.skilleffects = new int[effects.Count];
			for (int i = 0; i < effects.Count; i ++) {
				cfg.skilleffects[i] = (int)effects[i];
			}
			m_SkillSummons.Add(cfg.id, cfg);
		}

		// buff
		asset = ResourceManager.Instance.LoadAsset("cfg/buff") as TextAsset;
		data = JsonMapper.ToObject(asset.text);
		foreach (string key in data.Keys) {
			JsonData value = data [key];
			CfgBuff cfg = new CfgBuff();
			cfg.id = int.Parse (key);
			cfg.name = (string)value["name"];
			cfg.prefab =  (string)value["prefab"];
			cfg.life = (int)value["life"]/1000f;
			cfg.operation =  (BuffOperation)(int)value["operation"];
			JsonData args = value["args"];
			cfg.args = new int[args.Count];
			for (int i = 0; i < args.Count; i ++) {
				cfg.args[i] = (int)args[i];
			}
			m_Buffs.Add(cfg.id, cfg);
		}
	}
}
