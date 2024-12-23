using UnityEngine;

public class ShieldUpgradeItem : MonoBehaviour
{
    private float invincibilityDuration = 5f;
    public float lifetime = 4f; // Time before the item is destroyed

    private void Start()
    {
        // Destroy the item after time, if not collected
        Destroy(gameObject, lifetime);
    }

    // If item collides with player, player becomes invincible for 5 seconds
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ActivateShield(invincibilityDuration);
            }

            Destroy(gameObject); 
        }
    }
}
