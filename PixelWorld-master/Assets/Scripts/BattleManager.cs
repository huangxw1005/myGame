using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour {

	private static BattleManager instance;
	public static BattleManager GetInstance() {
		GameObject main = GameObject.Find("Main");
		if (main == null) {
			main = new GameObject("Main");
			DontDestroyOnLoad(main);
		}
	
		if (instance == null) {
			instance = main.AddComponent<BattleManager>();
		}
		return instance;
	}

	public object[] CallMethod(string func, params object[] args) {
		return Util.CallMethod("battle", func, args);
	}

	public void PlayerEnterNpc(int id, int attackid){
		CallMethod("player_enter_npc", id, attackid);
	}

	public void PlayerHit(int id, int attackid){
		CallMethod("player_hit", id, attackid);
	}

	public void EnemyHit(int id, int attackid){
		CallMethod("enemy_hit", id, attackid);
	}

	public void PlayerBreak(int id, Vector3 pos){
		CallMethod("player_break", id, pos);
	}

	public void PlayerTakeItem(int id, int attackid){
		CallMethod("player_take_item", id, attackid);
	}

	public Enemy SpawnEnemy(int id, Vector3 pos, Quaternion rot){
		if (CfgManager.GetInstance ().Monsters.ContainsKey (id)) {
			string prefab = CfgManager.GetInstance ().Monsters [id].prefab;
			Enemy enemy = CharacterManager.Instance.AddEnemy (prefab, pos, rot);
			CallMethod("enemy_spawn", enemy);
			return enemy;
		}
		return null;
	}

	public void HPChange(int value, int max){
		CallMethod("hp_change", value, max);
	}
	public void MPChange(int value, int max){
		CallMethod("sp_change", value, max);
	}
	public void ActorAddHP(Character actor, int value){
		CallMethod("add_hp", actor, value);
	}
	public void ActorAddSP(Character actor, int value){
		CallMethod("add_sp", actor, value);
	}
	public void ShowTip(string str){
		CallMethod("show_tip", str);
	}
	public void CastSkill(int id){
		CallMethod("cast_skill", id);
	}
}