using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Vector2 lastMoveDirection = Vector2.right; // Default to right direction

    // Weapon upgrade variables
    public int weaponLevel = 1; // Default weapon level
    public int maxWeaponLevel = 3; // Maximum weapon level

    // Shooting variables
    public float shootCooldown = 2f; // Cooldown time
    public float bulletSpeed = 10f;
    private bool canShoot = true;
    public Transform shootingPoint; // The point from which the bullets are shot
    public GameObject bulletPrefab; // The bullet GameObject to instantiate

    void Update()
    {
        // Detect if the player presses the spacebar to shoot
        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            Shoot();
        }
    }

    // This method should be called from PlayerMovement to update the last move direction
    public void UpdateMoveDirection(Vector2 moveDirection)
    {
        if (moveDirection != Vector2.zero)
        {
            lastMoveDirection = moveDirection;
        }
    }

    void Shoot()
    {
        if (lastMoveDirection == Vector2.zero)
        {
            lastMoveDirection = Vector2.right;
        }

        // Shoot based on the current weapon level
        switch (weaponLevel)
        {
            case 1:
                FireSingleShot(); // Shoots a single bullet to the right
                break;
            case 2:
                FireSplitShot();  // Shoots two bullets, each at a 30 degree angle up and down
                break;
            case 3:
                FireTripleShot(); // Shoots three bullets, combining the single and split shots
                break;
        }

        // Start the cooldown after shooting
        StartCoroutine(ShootingCooldown());
    }

    IEnumerator ShootingCooldown()
    {
        canShoot = false; // Disable shooting
        yield return new WaitForSeconds(shootCooldown); // Wait for the cooldown time
        canShoot = true; // Enable shooting
    }

    // Fire a single bullet straight in the current direction
    void FireSingleShot()
    {
        InstantiateBullet(lastMoveDirection, 0);
    }

    // Fire two bullets at 30 degree angles up and down from the current direction
    void FireSplitShot()
    {
        float angleOffset = 30f;

        // First bullet at +30 degrees (up)
        InstantiateBullet(lastMoveDirection, angleOffset);

        // Second bullet at -30 degrees (down)
        InstantiateBullet(lastMoveDirection, -angleOffset);
    }

    // Fire three bullets: one straight, one at +30 degrees, one at -30 degrees
    void FireTripleShot()
    {
        float angleOffset = 30f;

        // Fire a bullet straight in the current direction
        FireSingleShot();

        // Fire a bullet at +30 degrees
        InstantiateBullet(lastMoveDirection, angleOffset);

        // Fire a bullet at -30 degrees
        InstantiateBullet(lastMoveDirection, -angleOffset);
    }

    // Helper function to instantiate a bullet with angle relative to movement
    void InstantiateBullet(Vector2 direction, float angleOffset)
    {
        // Calculate the angle of the current direction
        float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the angle offset to get the new rotation
        float finalAngle = baseAngle + angleOffset;
        Quaternion bulletRotation = Quaternion.Euler(0, 0, finalAngle);

        // Instantiate the bullet at shooting point with the calculated direction
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, bulletRotation);

        // Set the bullets speed
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            Vector2 bulletDirection = new Vector2(Mathf.Cos(finalAngle * Mathf.Deg2Rad), Mathf.Sin(finalAngle * Mathf.Deg2Rad));
            bulletScript.SetSpeed(bulletDirection.normalized, bulletSpeed);
        }
    }

    // Function to upgrade the weapon level
    public void UpgradeWeapon()
    {
        if (weaponLevel < maxWeaponLevel)
        {
            weaponLevel++;
        }
    }
}
