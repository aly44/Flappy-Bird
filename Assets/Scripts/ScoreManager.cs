using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;

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
        scoreText.text = currentScore.ToString();
    }

    public void ResetScore()
    {
        currentScore = 0;
        scoreText.text = "0";
    }

    public int GetScore()
    {
        return currentScore;
    }
}
