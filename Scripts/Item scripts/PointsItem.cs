using UnityEngine;

public class PointsItem : MonoBehaviour
{
    public int pointsToGive = 50;
    public float lifetime = 4f; // Time before the item is destroyed

    private void Start()
    {
        // Destroy the item after time, if not collected
        Destroy(gameObject, lifetime);
    }

    // If item collides with player, points get added to the score
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddPoints(pointsToGive);
            }

            Destroy(gameObject);
        }
    }
}
