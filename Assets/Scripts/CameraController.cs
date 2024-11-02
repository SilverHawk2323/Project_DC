using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 1f;

    float currentRotationHorizontal;
    float currentRotationVertical;

    public Transform playerTransform;
    public float playerEyeLevel = 0.5f;

    public float verticalRotationMin;
    public float verticalRotationMax;

    private void Start()
    {
        currentRotationHorizontal = transform.localEulerAngles.y;
        currentRotationVertical = transform.localEulerAngles.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        currentRotationHorizontal += Input.GetAxis("Mouse X") * sensitivity;
        //Unity = +ve Y is up. Screen space = -ve Y is up.
        currentRotationVertical -= Input.GetAxis("Mouse Y") * sensitivity;

        //Value we want to clamp, Smallest number first, highest number last
        currentRotationVertical = Mathf.Clamp(currentRotationVertical, verticalRotationMin, verticalRotationMax);

        //apply the rotation to the camera object
        transform.localEulerAngles = new Vector3(currentRotationVertical, currentRotationHorizontal, 0);

        //snap the camera to the player's eye level + position
        transform.position = playerTransform.position + Vector3.up * playerEyeLevel;
    }
}
