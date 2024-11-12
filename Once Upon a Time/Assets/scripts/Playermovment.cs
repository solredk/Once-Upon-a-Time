using UnityEngine;
using UnityEngine.InputSystem;

public class Playermovment : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 moveDirection;
    public float movementSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * movementSpeed;
    }

    public void DoMoving(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>(); 
        moveDirection = new Vector3(input.x, 0, input.y); 
    }
}
