using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script makes sure movement only happens inside the screen
 */
public class ScreenBoundary : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        // Calculate screen boundaries based on camera view
        Camera cam = Camera.main;
        screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));

        // Get objects width and height
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    void LateUpdate()
    {
        // Get current position of the object
        Vector3 position = transform.position;

        // Clamp objects position within the screen boundaries
        position.x = Mathf.Clamp(position.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        position.y = Mathf.Clamp(position.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);

        // Apply clamped position
        transform.position = position;
    }
}
