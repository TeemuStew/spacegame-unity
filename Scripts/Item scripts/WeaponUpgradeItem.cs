using UnityEngine;

public class WeaponUpgradeItem : MonoBehaviour
{
    public float lifetime = 4f; // Time before the item is destroyed

    private void Start()
    {
        // Destroy the item after time, if not collected
        Destroy(gameObject, lifetime);
    }

    // If item collides with player, weapon gets upgraded till level 3
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerAttack playerAttack = collision.GetComponent<PlayerAttack>();
            if (playerAttack != null)
            {
                playerAttack.UpgradeWeapon();
            }

            Destroy(gameObject);
        }
    }
}
