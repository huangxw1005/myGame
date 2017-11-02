//
//	发射物
//
using UnityEngine;
using System.Collections;

public enum MissileType {
	ONCE,
	LONG,

}

public class Missile : MonoBehaviour {

	public int ID {get; set;}			// uid
	public int OwnID {get; set;}		// own id

	public MissileType Type;
	public float Speed = 4.0f;			// 移动速度
	public float Life = 10.0f;

	private Rigidbody _rigidbody;
	private float timer;

	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody>();
	}

	void OnEnable() {
		timer = 0;
	}

	void OnDisable() {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > Life) {
			ObjectPool.Recycle(gameObject);
		}
		_rigidbody.velocity = transform.forward * Speed;
	}


	void OnTriggerEnter(Collider collider)   { 
		Debug.Log("Missile:OnTriggerEnter");

		if (Type == MissileType.ONCE) {
			ObjectPool.Recycle(gameObject);
		}
	}
}
