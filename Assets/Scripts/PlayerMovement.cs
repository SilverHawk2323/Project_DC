using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float currentWalkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpPower;
    [SerializeField] float stamina = 6f;

    [SerializeField] Vector3 moveDirection;
    [SerializeField] Vector2 input;

    public Slider staminaBar;

    public CharacterController controller;

    public Transform cameraTransform;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        currentWalkSpeed = walkSpeed;
    }

    private void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        moveDirection.x = input.x * currentWalkSpeed;
        moveDirection.z = input.y * currentWalkSpeed;

        if (Input.GetKey(KeyCode.LeftShift) && input.y != 0 || input.x != 0)
        {
            Sprint();
        }
        else
        {
            currentWalkSpeed = walkSpeed;
            if (stamina < 6f)
            {
                stamina += Time.deltaTime;
            }
            staminaBar.value = stamina;
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

    public void Sprint()
    {
        if (stamina > 0f)
        {
            currentWalkSpeed = sprintSpeed;
            stamina -= Time.deltaTime;
            staminaBar.value = stamina;
        }
        else if (stamina <= 0f)
        {
            Debug.Log("Out of Stamina");
        }

    }
}
