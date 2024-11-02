using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float currentWalkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpPower;

    [SerializeField] Vector3 moveDirection;
    [SerializeField] Vector2 input;

    public CharacterController controller;

    public Transform cameraTransform;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        moveDirection.x = input.x * currentWalkSpeed;
        moveDirection.z = input.y * currentWalkSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveDirection.x = input.x * sprintSpeed;
            moveDirection.z = input.y * sprintSpeed;
        }
        else
        {
            currentWalkSpeed = walkSpeed;
        }

        moveDirection.y -= gravity * Time.deltaTime;

        if (controller.isGrounded)
        {
            moveDirection.y = Mathf.Clamp(moveDirection.y, -gravity, float.PositiveInfinity);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpPower;
            }
        }

        transform.localEulerAngles = new Vector3(0, cameraTransform.localEulerAngles.y, 0);

        moveDirection = transform.TransformDirection(moveDirection);
        controller.Move(moveDirection * Time.deltaTime);

    }
}
