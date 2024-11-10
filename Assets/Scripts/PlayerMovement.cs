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
    [SerializeField] private float speedThisFrame;
    [SerializeField] float stamina = 6f;
    public LayerMask groundedMask;
    [SerializeField] Vector3 movementThisFrame;
    [SerializeField] Vector2 inputThisFrame;

    public Slider staminaBar;

    public Rigidbody rb;

    public Transform cameraTransform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentWalkSpeed = walkSpeed;
    }

    private void Update()
    {
        //get our inputs this frame
        inputThisFrame.x = Input.GetAxis("Horizontal");
        inputThisFrame.y = Input.GetAxis("Vertical");

        //reset our potential movement to 0, 0, 0
        movementThisFrame = Vector3.zero;

        //apply our new input direction right/left and forward/back
        movementThisFrame.x = inputThisFrame.x;
        movementThisFrame.z = inputThisFrame.y;

        //figure out what our speed should be this frame
        speedThisFrame = walkSpeed;

        if (Input.GetKey(KeyCode.LeftShift) && (movementThisFrame.z != 0 || movementThisFrame.x != 0))
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
        movementThisFrame *= speedThisFrame;

        movementThisFrame.y = rb.velocity.y - gravity * Time.deltaTime;
        if (IsGrounded())
        {


            if (Input.GetKeyDown(KeyCode.Space))
            {
                movementThisFrame.y = jumpPower;
            }
        }


        Movement(movementThisFrame);

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 5f))
            {
                if (hit.transform.TryGetComponent<Interactable_MasterClass>(out Interactable_MasterClass interact))
                {
                    interact.Click();
                }
            }
        }

    }

    public void Sprint()
    {
        if (stamina > 0f)
        {
            speedThisFrame = sprintSpeed;
            stamina -= Time.deltaTime;
            staminaBar.value = stamina;
        }
        else if (stamina <= 0f)
        {
            Debug.Log("Out of Stamina");
        }

    }
    private bool IsGrounded()
    {
        //return the result of a raycast (true of false)
        return Physics.Raycast(transform.position, Vector3.down, 1.04f, groundedMask);
    }

    private void Movement(Vector3 movement)
    {
        transform.localEulerAngles = new Vector3(0, cameraTransform.localEulerAngles.y, 0);

        movement = transform.TransformDirection(movement);

        rb.velocity = movement;
    }
}
