using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private BirdController birdController;
    [SerializeField] private PipeSpawner pipeSpawner;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

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
        cameraShake.Shake();

        ScoreManager.Instance.UpdateHighScore();

        finalScoreText.text = "Score: " + ScoreManager.Instance.GetScore().ToString();
        highScoreText.text = "Best: " + ScoreManager.Instance.GetHighScore().ToString();

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

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
