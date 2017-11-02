using UnityEngine;
using LuaInterface;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

/// <summary>
/// Cold down behaviour.
/// 技能冷却处理
/// </summary>
public class ColdDownBehaviour : MonoBehaviour {

	public Image img;
	public Image imgMask;

	float totalTime = 0f;
	float timer = 0f;

        void Awake() {

        }

        public void SetColdDown(float time) {
		totalTime = time;
		timer = time;
        }

        public void ClearColdDown() {
        	timer = 0;
        	imgMask.fillAmount = 0;
        }

        void Update() {
		if (timer > 0) {
			timer -= Time.deltaTime;
			if (timer < 0f) timer = 0f;
			imgMask.fillAmount = timer / totalTime;
        	}
        }
}