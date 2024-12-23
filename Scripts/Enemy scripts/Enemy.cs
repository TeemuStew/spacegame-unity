using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy attributes
    public float moveSpeed = 2f;
    public int health = 1;

    // Item drop array
    public GameObject[] itemPrefabs; // Assigned in the inspector

    // Particle effect for when the enemy dies
    public GameObject deathEffect;

    void Update()
    {
        MoveLeft();

        // Destroy enemy if it goes off screen
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }

    // Move the enemy to the left
    protected virtual void MoveLeft()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    // Function to take damage
    public virtual void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            DropItem();

            // Instantiate the death particle effect at the enemy's position
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }

    // Handle item drop
    void DropItem()
    {
        // 20% chance of drop
        float dropChance = 0.2f; 
        if (Random.Range(0f, 1f) <= dropChance && itemPrefabs.Length > 0)
        {
            // Randomly select an item from the 3 types
            int randomIndex = Random.Range(0, itemPrefabs.Length);
            Instantiate(itemPrefabs[randomIndex], transform.position, Quaternion.identity);
        }
    }
}