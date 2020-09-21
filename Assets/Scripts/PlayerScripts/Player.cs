using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 8f, maxVelocity = 4f;

//	[SerializeField]
	private Rigidbody2D myBody;
	private Animator anim;

	void Awake () {
		myBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate () {
		PlayerMoveKeyBoard ();
	}

	float MoveInADirection(float movement, float directionSpeed){
		float forceX = directionSpeed;
		Vector3 temp = transform.localScale;
		temp.x = movement;

		transform.localScale = temp;
		anim.SetBool ("Walk", true);

		return forceX;
	}


	void PlayerMoveKeyBoard (){
		float forceX = 0;

		float vel = Mathf.Abs (myBody.velocity.x);

		float h = Input.GetAxisRaw ("Horizontal");

		if (h > 0) {
			if (vel < maxVelocity) {
				forceX = MoveInADirection (1.3f, speed);
			}
		} else if (h < 0) {
			if (vel < maxVelocity) {
				forceX = MoveInADirection (-1.3f, -speed);
			}
		} else {
			anim.SetBool ("Walk", false);
		}

		myBody.AddForce (new Vector2(forceX, 0));
	}

}//Player
