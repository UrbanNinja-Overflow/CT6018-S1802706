using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class SimpleCameraController : MonoBehaviour
    {

    public float distance = 25f;

    public float mouse_Sens = 100f;

    public Transform player_Body;


    float xRotation = 0f;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {

        //camera follow mouse pos
        float mouseX = Input.GetAxis("Mouse X") * mouse_Sens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouse_Sens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player_Body.Rotate(Vector3.up * mouseX);

    }

}
