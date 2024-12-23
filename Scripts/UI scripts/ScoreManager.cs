using UnityEngine;
using UnityEngine.UI;

/*
 * This script manages scores
 */
public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;

    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}
