using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    public int mouseSens = 10;
    public Transform playerBody;
    public float xRot;
    public float min = -90f;
    public float max = 90f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens;   // so rotation will be equal to the camera/mouse movement 
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens;

        playerBody.Rotate(Vector3.up * mouseX);


        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, min, max);
        transform.localRotation = Quaternion.Euler(xRot, 0, 0);


    }
}
