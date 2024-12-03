using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] CharacterController characterController;

    [SerializeField] Transform groundedCheck;

    [SerializeField] LayerMask groundMask;
    [SerializeField] ClimbState climbState;

    
    [SerializeField] Animator animator;

    Vector2 input;
    Vector3 velocity;
    Vector3 horizontalMovement;


    [Header("movement")]
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float groundDistance = 0.7f;    
    [SerializeField] float gravity = -9.81f;
    [SerializeField] bool isGrounded;
    [SerializeField] float staminaSpeed;
    [SerializeField] float rotationSpeed = 3;
    [SerializeField] float offset;


    [Header("sprinting")]
    [SerializeField] float sprintSpeed = 10f;
    public float stamina = 100;
    [SerializeField] bool isSprinting;
    public bool tired;

    [Header("jumping")]
    [SerializeField] float jumpStrength = 5f;
    [SerializeField] float climbSpeed;
    [SerializeField] bool isClimbing;
    [SerializeField] bool isJumping;

    float speedvalue;
    float counter;

    private enum ClimbState
    {
        jumping,
        climbing

    }
    private void Start()
    {

    }
    private void Update()
    {
        isGrounded = Physics.Raycast(groundedCheck.position, Vector3.down, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        if (isClimbing && climbState == ClimbState.climbing)
        {
            Climbing();
        }
        if (isJumping)
        {
            jump();
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

        Animator();
    }

    private void Animator()
    {
        if (input.x != 0 || input.y != 0)
        {
            animator.SetFloat("speed", 1);
        }
        else
        {
            animator.SetFloat("speed", 0);
        }
        if (isJumping)
        {
            animator.SetTrigger("Jump");
        }
    }

    public void DoMoving(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        Move();
    }
    private void jump()
    {
        velocity.y = Mathf.Sqrt(jumpStrength * -2f * gravity);
        isJumping = false;
    }
    public void DoJumping(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded&& climbState == ClimbState.jumping)
        {
            isJumping = true;
            // animator.SetBool("jumping", true);
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
            animator.SetBool("climbing", true);
        }
        else if (!isClimbing)
        {
            animator.SetBool("climbing", false);
        }
        if (!isClimbing && isGrounded)
        {
            climbState = ClimbState.jumping;
        }
    }

    public void DoRunning(InputAction.CallbackContext context)
    {
        if (context.performed && stamina > 0 && !tired)
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
        Vector3 direction = new Vector3(input.x, 0, input.y).normalized; //hij berekent de richting die hij input 

        if (direction.magnitude >= 0.1f)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, offset, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime); //hij roteert de richting waar hij naaroe gaatw

            horizontalMovement = direction * movementSpeed;
        }
        else
        {
            horizontalMovement = Vector3.zero; 
        }
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            transform.position = new Vector3(0, 0, 0);
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
