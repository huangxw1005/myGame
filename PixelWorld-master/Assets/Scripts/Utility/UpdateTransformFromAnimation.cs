using UnityEngine;


public class UpdateTransformFromAnimation : MonoBehaviour
{
	public Character target;

	bool wasPlaying;
	public bool bComplete {get {return wasPlaying == true;}}

	CharacterController characterController;
	Animation ani;
	Transform aniTrans;

	void Awake()
	{
		ani = GetComponentInChildren<Animation>();
		aniTrans = ani.transform;
		wasPlaying = false;
	}

	void Start() {
		characterController = target.GetComponent<CharacterController> ();
	}

	void LateUpdate()
	{
		if (target == null || characterController == null)
			return;
		
		if (!ani.isPlaying && !wasPlaying) 
			return;

		Vector3 move = aniTrans.position - target.transform.position;
		characterController.Move (move);

		wasPlaying = ani.isPlaying;
	}
}