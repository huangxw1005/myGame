//
//	可破坏物品基类
//
using UnityEngine;
using System.Collections;


public abstract class BreakableObject : MonoBehaviour {

	// broken后，移除（0:不移除）
	public float LifeTime;

	public int ID {get; set;}			// uid

	public bool IsBroken {get; set;}

	// Use this for initialization
	protected virtual void Start () {
	 	IsBroken = false;
	}

	protected virtual void OnEnable() {
	}

	protected virtual void OnDisable() {
	}

	void OnTriggerEnter(Collider collider)   { 
		Debug.Log("BreakableObject:OnTriggerEnter");
		if (IsBroken) return;

		string tag = collider.gameObject.tag;
		if (tag == "PlayerWeapon") {
			IsBroken = true;
			OnBreaking();

			Player player = collider.transform.parent.GetComponent<Player>();
			BattleManager.GetInstance ().PlayerBreak (ID, transform.position);

			if (LifeTime > 0) StartCoroutine(delayForDestroy(LifeTime));
		}
	}

	IEnumerator delayForDestroy(float time) {
		yield return new WaitForSeconds(time);

		Destroy(gameObject);
	}

	protected abstract void OnBreaking();
}
