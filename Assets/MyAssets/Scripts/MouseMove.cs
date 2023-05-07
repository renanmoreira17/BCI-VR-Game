using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    private float xRotationCamera = 0f;
    private float yRotationCamera = 0f;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Start()
    {

    }

    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        // yaw = Mathf.Clamp(yaw, -90f, 90f);
        // //the rotation range
        // pitch = Mathf.Clamp(pitch, -60f, 90f);
        // //the rotation range

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);


        // float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        // float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // xRotationCamera -= mouseY;
        // xRotationCamera = Mathf.Clamp(xRotationCamera, -90f, 90f);
        // yRotationCamera += mouseX;
        // yRotationCamera = Mathf.Clamp(xRotationCamera, -90f, 90f);

        // transform.localRotation = Quaternion.Euler(xRotationCamera, yRotationCamera, 0f);

    }
}
