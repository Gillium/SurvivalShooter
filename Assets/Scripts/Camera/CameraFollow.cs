using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;		//the position that the camera will be following.
	public float smoothing = 5f;	//the speed with which the camera will be following.

	Vector3 offset;					//the initial offset from the target.

	void Start()
	{
		//calculates the initial offset.
		offset = transform.position - target.position;
	}

	void FixedUpdate()
	{
		//creates a position the camera is aiming for based on the offset from the target.
		Vector3 targetCamPos = target.position + offset;

		//smoothes interpolate between the camera's current position and it's target position.
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
