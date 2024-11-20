using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] CharacterController characterController;

    [SerializeField] Transform groundedCheck;

    [SerializeField] LayerMask groundMask;
    [SerializeField] ClimbState climbState;

    Vector2 input;
    Vector3 velocity;
    Vector3 horizontalMovement;

    [Header("movement")]
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float groundDistance = 0.7f;    
    [SerializeField] float gravity = -9.81f;
    [SerializeField] bool isGrounded;
    [SerializeField] float staminaSpeed;
    [Header("sprinting")]
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float stamina = 100;
    [SerializeField] bool isSprinting;
    [SerializeField] bool tired;

    [Header("jumping")]
    [SerializeField] float jumpStrength = 5f;
    [SerializeField] float climbSpeed;
    [SerializeField] bool isClimbing;

    [SerializeField] Vector3 startpositie;
    float counter;

    private enum ClimbState
    {
        jumping,
        climbing

    }
    private void Start()
    {
        startpositie= transform.position;
    }
    private void Update()
    {

        isGrounded = Physics.Raycast(groundedCheck.position, Vector3.down, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;  
        }
        else
        {
            velocity.y += gravity * Time.deltaTime; 
        }
        
        if (isClimbing && climbState == ClimbState.climbing)
        {
            Climbing();
        }

        Move();
        
        Stamina();
    }

    private void Stamina()
    {
        if (!tired&& isSprinting && stamina >= 0)
        {
            counter += Time.deltaTime * staminaSpeed;
            if (counter > 0.1f)
            {
                stamina--;
                counter = 0;
            }
            if (stamina == 0)
            {
                tired = true;
            }
        }

        else if (!isSprinting && stamina < 100)
        {
            counter += Time.deltaTime * staminaSpeed;
            if (counter > 0.1f)
            {
                stamina++;
                counter = 0;
            }
        }
        else if (tired)
        {
            movementSpeed = 5f;
            counter += Time.deltaTime * staminaSpeed;
            if (counter > 0.1f)
            {
                stamina++;
                counter = 0;
            }
            if (stamina == 100)
            {
                tired = false;
            }
        }
    }

    public void DoMoving(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        Move();
    }

    public void DoJumping(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded&& climbState == ClimbState.jumping)
        {
            velocity.y = Mathf.Sqrt(jumpStrength * -2f * gravity);  
        }
        else if (context.performed && isGrounded && climbState == ClimbState.climbing)
        {
            isClimbing = true;

        }
        if (context.canceled)
        {
            isClimbing = false;
        }
    }

    private void Climbing()
    {
        if (isClimbing && climbState == ClimbState.climbing)
        {
             velocity.y = climbSpeed;           
        }
    }

    public void DoRunning(InputAction.CallbackContext context)
    {
        if (context.performed && stamina > 0 && !tired&&isGrounded)
        {
            isSprinting = true;
            movementSpeed = sprintSpeed;
            counter -= Time.deltaTime * staminaSpeed;
        }
        else if (context.canceled || stamina == 0||tired)
        {
            isSprinting = false;
            movementSpeed = 5f;
        }
    }

    private void Move()
    {
        horizontalMovement = new Vector3(input.x, 0, input.y).normalized * movementSpeed;
        Vector3 finalMovement = horizontalMovement + new Vector3(0, velocity.y, 0);
        characterController.Move(finalMovement * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("climbable"))
        {
            climbState = ClimbState.climbing;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("climbable"))
        {
            climbState = ClimbState.jumping;
        }
    }
}
