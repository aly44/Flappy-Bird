using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private BirdController birdController;
    [SerializeField] private PipeSpawner pipeSpawner;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private bool isGameOver;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void OnBirdDied()
    {
        if (isGameOver)
        {
            return;
        }

        isGameOver = true;
        pipeSpawner.StopSpawning();
        finalScoreText.text = "Score: " + ScoreManager.Instance.GetScore().ToString();
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");

        foreach (GameObject pipe in pipes)
        {
            Destroy(pipe);
        }

        isGameOver = false;
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
        ScoreManager.Instance.ResetScore();
        birdController.ResetBird();
        pipeSpawner.StartSpawning();
    }
}
