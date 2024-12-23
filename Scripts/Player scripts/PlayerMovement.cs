using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 movement;

    private PlayerAttack playerAttack;

    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>(); 
    }

    void Update()
    {
        // Input from player (WASD or arrows)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Update move direction in PlayerAttack
        if (movement != Vector2.zero)
        {
            playerAttack.UpdateMoveDirection(movement);
        }
    }

    void FixedUpdate()
    {
        // Move the player using Rigidbody2D's velocity
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    // Get the current movement direction
    public Vector2 GetMovementDirection()
    {
        return movement;
    }
}
