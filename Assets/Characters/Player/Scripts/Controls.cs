using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

	Animator animator;
	CharacterController controler;

	public int dir = 1;
	public int lastdir = 1;

	bool run;
	bool lastrun;

	bool gravity = true;

	bool grounded;
	int groundlessframes;

	Vector2 velocity;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		controler = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

		if (controler.isGrounded) {
			grounded = true;
			groundlessframes = 0;
		} else {
			groundlessframes++;
			if (groundlessframes > 50) {
				grounded = false;
				Debug.Log("NOGROUND!");
			}
		}



		Vector3 newPosition = transform.position;
		newPosition.z = 41.7f;
		transform.position = newPosition;


					


		animator.SetBool("Jump", Input.GetKey(KeyCode.Space));

		//animator.SetFloat("Forward",v);
		//animator.SetFloat("Strafe",h);


		if (grounded) {


			lastdir = dir;
			lastrun = run;
			run = false;

			if (Input.GetKey(KeyCode.A)) {
				dir = -1;
				run = true;
				animator.SetBool("Turn Left", false);
			}
			if (Input.GetKey(KeyCode.D)) {
				dir = 1;
				run = true;
				animator.SetBool("Turn Left", true);
			}

			if (dir != lastdir) {
				animator.SetBool("Change Direction", true);
			}


			animator.SetBool("Run", (run && lastrun));

			if (run) {
/*				if (dir > 0)
					transform.eulerAngles = new Vector3(0, 90, 0);

				if (dir < 0)
					transform.eulerAngles = new Vector3(0, 270, 0);
*/
			}



		
		} 


		if (gravity) {

			if (controler.isGrounded) velocity.y = 0;

			//velocity.y += Physics.gravity.magnitude / Time.deltaTime;
			//velocity.x *= 0.99f;
			velocity.x = 0f;
			velocity.y -= Physics.gravity.magnitude * Time.deltaTime * Time.deltaTime;

			//newPosition.x += velocity.x;
			//newPosition.y += velocity.y;



			controler.Move(new Vector3(velocity.x, velocity.y, 0f));

		}






	}

	public void OnDirChange() {
		Debug.Log("TURNING");

		Quaternion rotation = Quaternion.LookRotation(Vector3.right * dir);


		animator.MatchTarget(transform.position , rotation, AvatarTarget.Root, 
			new MatchTargetWeightMask(Vector3.zero, 1f), 0.45f, 0.95f);

		animator.SetBool("Change Direction", false);
	}

	public void GravityEnable() {
		gravity = true;
	}

	public void GravityDisable() {
		gravity = false;
	}

}
