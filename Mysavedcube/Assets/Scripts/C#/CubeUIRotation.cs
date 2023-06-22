using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeUIRotation : CubeRoll
{
    public float rotationSpeed = 500f; // Adjust this value to control the rotation speed

    private void OnMouseDrag()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotationX = mouseY * rotationSpeed * Time.deltaTime;
        float rotationY = -mouseX * rotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.right, rotationX, Space.World);
        transform.Rotate(Vector3.up, rotationY, Space.World);
    }
}
