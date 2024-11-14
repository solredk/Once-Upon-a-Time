using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovment : MonoBehaviour
{
    float counter;

    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float jumpStrength = 5f;
    [SerializeField] float stamina = 100;
    [SerializeField] float gravity = -9.81f; // Standard gravity
    [SerializeField] float groundDistance = 0.7f;

    [SerializeField] bool isGrounded;
    [SerializeField] bool isSprinting;
    [SerializeField] bool tired;

    [SerializeField] Transform groundedCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] CharacterController characterController;

    Vector2 input;
    Vector3 velocity;
    Vector3 horizontalMovement;

    void Start()
    {
        //if (!characterController) characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundedCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;  
        }
        else
        {
            velocity.y += gravity * Time.deltaTime; 
        }

        Move();
        Stamina();
    }

    private void Stamina()
    {
        if (isSprinting && stamina > 0)
        {
            counter += Time.deltaTime;
            if (counter > 0.1f)
            {
                stamina--;
                tired = stamina <= 0;
                counter = 0;
            }
        }
        else if (!isSprinting && stamina < 100)
        {
            counter += Time.deltaTime;
            if (counter > 0.1f)
            {
                stamina++;
                tired = stamina >= 100;
                counter = 0;
            }
        }
    }

    public void DoMoving(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        horizontalMovement = new Vector3(input.x, 0, input.y).normalized * movementSpeed;
    }

    public void DoJumping(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpStrength * -2f * gravity);  
        }
    }

    public void DoRunning(InputAction.CallbackContext context)
    {
        if (context.performed && !isSprinting && !tired)
        {
            isSprinting = true;
            movementSpeed = sprintSpeed;
        }
        else if (context.canceled || stamina == 0)
        {
            isSprinting = false;
            movementSpeed = 5f;
        }
    }

    private void Move()
    {
        Vector3 finalMovement = horizontalMovement + new Vector3(0, velocity.y, 0);
        characterController.Move(finalMovement * Time.deltaTime);
    }
}
