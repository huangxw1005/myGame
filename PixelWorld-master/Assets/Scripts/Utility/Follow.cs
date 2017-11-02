using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	public Transform target;
	public Vector3 offset;

	private RectTransform rectTransform;

	void Start () {
		rectTransform = GetComponent<RectTransform>();

		Update();
	}

	void Update () {

		if (target == null || rectTransform == null) {
		    return;
		}

		Vector3 targetScreenPosition = Camera.main.WorldToScreenPoint(target.position + offset);

		//����һ������ת��Ϊ UI  2D ��������ı���
		Vector3 followPosition;
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, targetScreenPosition, null, out followPosition)) {
			transform.position = followPosition;
		}
	}
}