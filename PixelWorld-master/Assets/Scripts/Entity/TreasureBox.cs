//
//	发射物
//
using UnityEngine;
using System.Collections;

public class TreasureBox : BreakableObject {

	protected override void OnBreaking() {
		GetComponentInChildren<Animation>().Play();
	}


}
