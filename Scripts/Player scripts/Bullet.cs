using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;

    private ScoreManager scoreManager;

    public float lifetime = 4f;

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy bullet after lifetime, if it doesn't hit anything
        rb = GetComponent<Rigidbody2D>();

        scoreManager = FindObjectOfType<ScoreManager>();

    }

    // Set the bullet speed when instantiated
    public void SetSpeed(Vector2 direction, float speed)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed; // Set velocity based on the direction and speed passed in
    }

    // Bullet collision with enemy
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Check if the object hit is an enemy
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage();

            // Add points to the score when an enemy is destroyed
            if (scoreManager != null)
            {
                scoreManager.AddPoints(100);
            }

            Destroy(gameObject);
        }
    }
}