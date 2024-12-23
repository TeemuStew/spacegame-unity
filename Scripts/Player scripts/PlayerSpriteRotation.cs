using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is to make simple animation to make player sprite turn to the the direction it's moving
 */

public class PlayerSpriteRotation : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private SpriteRenderer spriteRenderer;

    // Offset to adjust the rotation for sprite
    public float rotationOffset = -360f;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Only rotate if player is moving
        if (playerMovement != null && playerMovement.GetMovementDirection() != Vector2.zero)
        {
            RotateSprite(playerMovement.GetMovementDirection());
        }
    }

    // Rotate sprite based on movement direction
    void RotateSprite(Vector2 movementDirection)
    {
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rotationOffset)); 
    }
}
