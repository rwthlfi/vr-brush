using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour

{
    public float sensitivity = 2.0f; // Mouse sensitivity
    public float speed = 5.0f; // Movement speed

    private void Update()
    {
        // Mouse input for looking around
        float mouseX = Input.GetAxis("Mouse Y") * sensitivity;
        float mouseY = Input.GetAxis("Mouse X") * sensitivity;

        Vector3 fixedRotation = new Vector3 (mouseY, mouseX, 0);
        transform.Rotate(fixedRotation);


        float horizontal = 0;
        float vertical = 0;

        if (Keyboard.current.wKey.isPressed)
        {
            vertical = 1;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            vertical = -1;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            horizontal = -1;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            horizontal = 1;
        }

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        Vector3 movement = direction * speed * Time.deltaTime;


        transform.Translate(movement);

        // Debug information
        Debug.Log("Mouse X: " + mouseX + ", Mouse Y: " + mouseY);
        Debug.Log("Horizontal: " + horizontal + ", Vertical: " + vertical);
        Debug.Log("Camera Position: " + transform.position);
        Debug.Log("Camera Rotation: " + transform.rotation.eulerAngles);
    }
}