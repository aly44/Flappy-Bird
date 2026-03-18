using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private const string HIGH_SCORE_KEY = "HS_FlappyBird";

    [SerializeField] private SpriteScoreDisplay scoreDisplay;

    private int currentScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddScore()
    {
        currentScore++;
        scoreDisplay.UpdateDisplay(currentScore);
    }

    public void ResetScore()
    {
        currentScore = 0;
        scoreDisplay.UpdateDisplay(0);
    }

    public int GetScore()
    {
        return currentScore;
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
    }

    public void UpdateHighScore()
    {
        if (currentScore > GetHighScore())
        {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, currentScore);
            PlayerPrefs.Save();
        }
    }
}
