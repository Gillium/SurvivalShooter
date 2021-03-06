﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;	//the speed that the player will move at.

	Vector3 movement;			//the vector to store the direction of the player's movement.
	Animator anim;				//reference to the animator component.
	Rigidbody playerRigidbody;	//reference to the rigidbody.
	int floorMask;				//a layer mask so that a ray can be cast just at gameobjects on the floor layer.
	float camRayLength = 100f;	//the length of the ray from the camera into the scene.

	void Awake()
	{
		//creates the layer mask for the floor layer.
		floorMask = LayerMask.GetMask ("Floor");
		//sets up the references.
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate()
	{
		//stores the input axes.
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		//moves the player around the scene.
		Move (h, v);

		//turns the player to face the mouse cursor.
		Turning ();

		//animates the player.
		Animating (h, v);
	}

	void Move(float h, float v)
	{
		//sets the movmeent vector based on the axis input.
		movement.Set (h, 0f, v);

		//normalizes the movement vector and makes it proportional to the speed per second.
		movement = movement.normalized * speed * Time.deltaTime;

		//moves the player to tis current position pluss the movement.
		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning ()
	{
		//creates a ray from the mouse cursor on screen in the direction of the camera.
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		//creates a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;

		//performs the raycast and if it hits something on thefloor layer...
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			//creates a vector from the player to the point on the floor the raycast from the mouse hit.
			Vector3 playerToMouse = floorHit.point - transform.position;

			//ensures the vector is entirely along the floor plane.
			playerToMouse.y = 0f;

			//creates a rotation based on looking down the vector from the player to the mouse.
			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

			//sets the player's rotation to this new rotation.
			playerRigidbody.MoveRotation(newRotation);
		}
	}

	void Animating(float h, float v)
	{
		//creates a boolean that is true if either of the input axes is non-zero.
		bool walking = h != 0f || v != 0f;

		//tells the animator whether or not the player is walking.
		anim.SetBool("IsWalking", walking);
	}
}