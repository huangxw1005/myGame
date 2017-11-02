//
//	掉落道具
//
using UnityEngine;
using System.Collections;


public class DropItem : MonoBehaviour {

	public int ID {get; set;}			// uid
	public Transform Target {get;set;}

	// Use this for initialization
	void Start () {
	}

	void OnEnable() {
				
	}

	void OnDisable() {
		Target = null;
	}

	// Update is called once per frame
	void Update () {
		if (Target == null) return;

            	transform.position = Vector3.Lerp(transform.position, Target.position, 10*Time.deltaTime);
	}


	// 
	public void OnHit() {
		
	}

}
