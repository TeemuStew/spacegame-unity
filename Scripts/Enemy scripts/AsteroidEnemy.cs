using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidEnemy : Enemy
{
    public bool isSmallAsteroid = false; // To distinguish between normal and small asteroids
    public GameObject smallAsteroidPrefab;

    void Start()
    {
        moveSpeed = 2f; // Regular speed for asteroid enemy
    }

    public override void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
               base.TakeDamage(); // Call the base TakeDamage method to handle item drops

            // Split into smaller asteroids if it's a large asteroid
            if (!isSmallAsteroid && smallAsteroidPrefab != null)
            {
                // Instantiate two smaller asteroids at 45 degree angles
                Instantiate(smallAsteroidPrefab, transform.position, Quaternion.Euler(0, 0, 45));
                Instantiate(smallAsteroidPrefab, transform.position, Quaternion.Euler(0, 0, -45));
            }

            // Destroy the current asteroid after splitting
            Destroy(gameObject);
        }
    }
}
