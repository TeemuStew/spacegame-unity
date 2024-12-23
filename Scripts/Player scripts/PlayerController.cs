using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
   
    // Reference to the shield particle effect
    public GameObject shieldEffectPrefab;
    private GameObject activeShieldEffect;
    public float fadeDuration = 1f;
    private bool isInvincible = false;

    // Game over and score UI
    public GameObject gameOverUI;
    public Text finalScoreText;
    private ScoreManager scoreManager;
    private bool isGameOver = false;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void Update()
    {
        // Restart the game when SPACE is pressed
        if (isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    // When player collides with enemy, the game ends
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!isInvincible)
            {
                GameOver();
            }
        }
    }

    // Function to handle game over
    private void GameOver()
    {
        // Disable player movement and attacks
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;

        // Display the Game Over UI and final score
        gameOverUI.SetActive(true);

        int finalScore = scoreManager.GetScore();
        finalScoreText.text = "Final Score: " + finalScore.ToString();

        isGameOver = true;
        Time.timeScale = 0f; // Freeze time until the game is restarted
    }

    // Function to restart the game
    private void RestartGame()
    {
        Time.timeScale = 1f; // Reset time scale
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload current scene
    }

    // Method for when player collects a shield upgrade
    public void ActivateShield(float duration)
    {
        if (!isInvincible)
        {
            if (shieldEffectPrefab != null)
            {
                activeShieldEffect = Instantiate(shieldEffectPrefab, transform.position, Quaternion.identity, transform);
            }

            StartCoroutine(InvincibilityCoroutine(duration));
        }
    }

    private IEnumerator InvincibilityCoroutine(float duration)
    {
        isInvincible = true;

        yield return new WaitForSeconds(duration);

        isInvincible = false;

        // Start fading out the shield effect
        if (activeShieldEffect != null)
        {
            StartCoroutine(FadeOutShield());
        }
    }

    // Coroutine to fade out the shield effect
    private IEnumerator FadeOutShield()
    {
        SpriteRenderer shieldRenderer = activeShieldEffect.GetComponent<SpriteRenderer>();
        if (shieldRenderer != null)
        {
            Color shieldColor = shieldRenderer.color;
            float startAlpha = shieldColor.a;

            for (float t = 0f; t < fadeDuration; t += Time.deltaTime)
            {
                float normalizedTime = t / fadeDuration;
                shieldColor.a = Mathf.Lerp(startAlpha, 0f, normalizedTime);
                shieldRenderer.color = shieldColor;
                yield return null;
            }

            // Ensure the shield is fully invisible at the end
            shieldColor.a = 0f;
            shieldRenderer.color = shieldColor;

            Destroy(activeShieldEffect);
        }
    }
}