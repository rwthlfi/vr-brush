using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private float movementSpeed = 10.0f;
	private float rotationSpeed = 400.0f;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		Vector3 movement = new Vector3(Input.GetAxis("X"), Input.GetAxis("Y"), Input.GetAxis("Z"));
		movement.Normalize();
		movement *= movementSpeed * Time.deltaTime;

		Vector3 rotation = new Vector3( Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0.0f);
		rotation.Normalize();
		rotation *= rotationSpeed * Time.deltaTime;

		transform.Translate(movement);
		transform.eulerAngles += rotation;
	}
}