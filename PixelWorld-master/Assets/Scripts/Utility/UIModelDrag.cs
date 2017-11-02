using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIModelDrag : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler {

	public Transform targetModel;

	private float value;

	public void OnDrag(PointerEventData eventData) {

		float rotationY = targetModel.localEulerAngles.y - eventData.delta.x/2;

		targetModel.localEulerAngles = new Vector3(0, rotationY, 0);
	}

	public void OnPointerDown(PointerEventData eventData) {
		//transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
	}

	public void OnPointerUp(PointerEventData eventData) {
		//transform.localScale = new Vector3(1f, 1f, 1f);
	}

}
