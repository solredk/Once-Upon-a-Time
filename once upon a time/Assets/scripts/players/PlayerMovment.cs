using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovment : MonoBehaviour
{
    float counter;

    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float sprintSpeed = 10;
    [SerializeField] float jumpStrength;
    [SerializeField] float stamina = 100;
    [SerializeField] float gravity;

    [SerializeField] bool isGrounded;
    [SerializeField] bool isSprinting;
    [SerializeField] bool tired;

    [SerializeField] Transform groundedCheck;


    Rigidbody rb;
    Vector3 moveDirection;
    Vector3 jump;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0, 2, 0);
    }
    private void Update()
    {
        rb.velocity = moveDirection * movementSpeed;
        if (Physics.Raycast(groundedCheck.position, groundedCheck.TransformDirection(Vector3.down), .8f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        if (!isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y - gravity * Time.deltaTime, rb.velocity.z);
        }
        if (isSprinting && stamina>=1) 
        {
            counter += Time.deltaTime;

            if (counter > .1f)
            {
                stamina--;
                counter = 0;
            }
        }
        else if (!isSprinting&& stamina<=99)
        {
            counter += Time.deltaTime;

            if (counter > .1f)
            {
                stamina++;
                counter = 0;
            }
        }
        if (stamina==0)
        {
            tired = true;
        }
        if (tired)
        {
            if (stamina == 100)
            {
                tired = false;
            }
        }

    }

    public void DoMoving(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        moveDirection = new Vector3(input.x, 0, input.y);
    }

    public void DoJumping(InputAction.CallbackContext context)
    {
        if (context.performed&&isGrounded)
        {
            rb.AddForce(Vector3.up * jumpStrength );
            isGrounded = false;
        }
        
    }

    public void DoRunning(InputAction.CallbackContext context)
    {
        if (context.performed&& !isSprinting&&!tired) 
        {
            isSprinting = true;
            movementSpeed = 10;
        }
        if (context.canceled && isSprinting||stamina==0)
        {
            isSprinting = false;
            movementSpeed = 5;
        }
    }
}


