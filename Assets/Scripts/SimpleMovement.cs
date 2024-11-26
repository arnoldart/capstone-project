using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed of movement
    private Rigidbody2D rb;       // Reference to the Rigidbody2D component

    private Vector2 movement;     // Store movement input

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component attached to the player
    }

    private void Update()
    {
        // Get input from the player (A for left, D for right)
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Create a Vector2 movement direction
        movement = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    private void FixedUpdate()
    {
        // Apply the movement to the Rigidbody2D in FixedUpdate for physics
        rb.velocity = new Vector2(movement.x, rb.velocity.y);
    }
}
