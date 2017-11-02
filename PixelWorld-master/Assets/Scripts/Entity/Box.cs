//
//	发射物
//
using UnityEngine;
using System.Collections;

public class Box : BreakableObject {


	protected override void OnBreaking() {
		GetComponentInChildren<Animation>().Play();
	}

}
