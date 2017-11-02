using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : Singleton<CharacterManager> {

	private List<Character> m_Characters = new List<Character>();

	static int UID = 0;
	public Player Self = null;

	public Player AddPlayer(Vector3 pos, Quaternion rot) {
		Object prefab = ResourceManager.Instance.LoadAsset("Prefabs/Character/king");
		GameObject go = GameObject.Instantiate(prefab, pos, rot) as GameObject;
		//go.transform.localScale = Vector3.one;
		Player player = go.GetComponent<Player>();
		player.ID = UID++;
		m_Characters.Add(player);
		player.IsUser = true;
		return player;
	}

	public Player AddPlayer(float x, float y, float z) {
		return AddPlayer (new Vector3 (x, y, z), Quaternion.identity);
	}  

	public Enemy AddEnemy(string prefab, Vector3 pos, Quaternion rot) {
		Object obj = ResourceManager.Instance.LoadAsset(prefab);
		GameObject go = GameObject.Instantiate(obj, pos, rot) as GameObject;
		//go.transform.localScale = Vector3.one;
		Enemy enemy = go.GetComponent<Enemy>();
		enemy.ID = UID++;
		m_Characters.Add(enemy);
		return enemy;
	}

	public bool CheckEnemyInArea( Vector3 pos, float range) {
		
		foreach(Character ch in m_Characters) {
			if (ch is Enemy) {
				Vector3 offset = ch.transform.position - pos;
				if (offset.magnitude < range) {
					return true;
				}
			}
		}

		return false;
	}

	public Enemy FindNearestEnemy(Vector3 pos, out float distance) {
		Enemy ret = null;
		distance = float.MaxValue;
		foreach(Character ch in m_Characters) {
			if (ch is Enemy && ch.IsAlive()) {
				Vector3 offset = ch.transform.position - pos;
				if (offset.magnitude < distance) {
					distance = offset.magnitude;
					ret = ch as Enemy;
				}
			}
		}

		return ret;
	}

	public List<Character> FindActor(SkillTarget targetType) {
		List<Character> list = new List<Character>();

		foreach(Character ch in m_Characters) {
			if (ch is Enemy && ch.IsAlive()) {
				list.Add(ch);
			}
		}

		return list;
	}

	public List<Character> FindActor(Vector3 pos, SkillTarget targetType, SkillRegion region, float radius, float angle, Vector3 dir) {
		List<Character> list = FindActor(targetType);

		if (region == SkillRegion.SKILL_REGION_CIRCLE) {
			for (int i = list.Count-1; i >=0; i --) {
				Character actor = list[i];
				if (!MathUtil.CheckCircleIntersect(pos, radius, actor.transform.position, actor.GetRadius())) {
					list.RemoveAt(i);
				}
			}
		} else {
			for (int i = list.Count-1; i >=0; i --) {
				Character actor = list[i];
				if (!MathUtil.CheckSectorIntersect(pos, dir, angle/2, radius, actor.transform.position, actor.GetRadius())) {
					list.RemoveAt(i);
				}
			}
		}

		return list;
	}

	public List<Character> SortActor(Vector3 pos, List<Character> list) {
		List<Character> lst = new List<Character>();
		if(list == null)
			return lst;
		
		float fMin = float.MaxValue;
		int index = 0;
		for(int i = 0; i < list.Count;) {

			Vector3 targetPoint = list[i].transform.position;
			targetPoint = targetPoint - pos;
			float fDis = targetPoint.magnitude;
			if (fMin > fDis)
			{
				fMin = fDis;
				index = i;
			}
			
			i++;
			if(i == list.Count)
			{
				lst.Add(list[index]);
				list.RemoveAt(index);
				i = 0;
				index = 0;
				fMin = float.MaxValue;
			}
		}
		return lst;
	}

	public bool Remove(Character character) {
		Destroy(character.gameObject);
		return m_Characters.Remove(character);
	}

	public void RemoveAll() {
		foreach(Character ch in m_Characters) {
			if (ch != null) {
				Destroy(ch.gameObject);
			}
		}
		m_Characters.Clear();

		UID = 0;
	}
}