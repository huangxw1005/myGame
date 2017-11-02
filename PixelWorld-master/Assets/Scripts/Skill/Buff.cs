using UnityEngine;
using System.Collections;

/// <summary>
/// Buff.
/// buff实体（单体or群体）
/// </summary>
public class Buff
{
	private int id;

	CfgBuff cfg;

	private float timer					= 0f;
	private bool bPeriod 				= false; 
	private float periodTimer				= 0f;

	private bool isDead 					= false;

	private Character owner;

	public void SetOwner(Character actor) {
		owner = actor;
	}
	public bool IsDead() {
		return isDead;	
	}

	public static Buff CreateBuff(int id) {
		if (CfgManager.GetInstance().Buffs.ContainsKey(id)) {
			return new Buff(id);
		}
		return null;
	}

	public Buff(int id) {
		if (CfgManager.GetInstance().Buffs.ContainsKey(id) == false) {
			Debug.LogErrorFormat("buff cfg not found: {0}", id);
			return;
		}

		cfg = CfgManager.GetInstance().Buffs[id];
		this.id = cfg.id;

		if (cfg.operation == BuffOperation.OPERATION_HP || 
			cfg.operation == BuffOperation.OPERATION_SP || 
			cfg.operation == BuffOperation.OPERATION_DAMAGE) {
			bPeriod = true;
			periodTimer = cfg.life / cfg.args[0];
		}
	}


	public void Start() {
		Debug.Log("Buff:Start");
		if (cfg.prefab != null && cfg.prefab.Length > 0) {
			owner.AttackEffect(cfg.prefab, cfg.life, Vector3.zero);
		}

		switch(cfg.operation) {
		case BuffOperation.OPERATION_HP:
			break;
		case BuffOperation.OPERATION_SP:
			break;
		case BuffOperation.OPERATION_DAMAGE:
			break;
		case BuffOperation.OPERATION_ADD:
			break;
		case BuffOperation.OPERATION_REDUCE:
			break;
		case BuffOperation.OPERATION_STUN:
			owner.IsStun = true;
			break;
		case BuffOperation.OPERATION_FROZEN:
			owner.IsFrozen = true;
			break;
		default:
			break;
		}
	}

	public void Update () {

		timer += Time.deltaTime;
		if (timer > cfg.life) {
			End();
		}

		// 持续buff
		if (bPeriod) {
			
		}
	}


	// 攻击
	void OnAttack(Character actor) {
	}

	// 攻击结束
	void OnAttackEnd(Character actor) {
	}

	void End() {
		Debug.Log("Buff:End");
		isDead = true;

		switch(cfg.operation) {
		case BuffOperation.OPERATION_HP:
			break;
		case BuffOperation.OPERATION_SP:
			break;
		case BuffOperation.OPERATION_DAMAGE:
			break;
		case BuffOperation.OPERATION_ADD:
			break;
		case BuffOperation.OPERATION_REDUCE:
			break;
		case BuffOperation.OPERATION_STUN:
			owner.IsStun = false;
			break;
		case BuffOperation.OPERATION_FROZEN:
			owner.IsFrozen = false;
			break;
		default:
			break;
		}
	}
}