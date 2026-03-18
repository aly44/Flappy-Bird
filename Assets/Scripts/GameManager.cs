using UnityEngine;
using TMPro;

public enum GameState
{
    Idle,
    Playing,
    Dead
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private BirdController birdController;
    [SerializeField] private PipeSpawner pipeSpawner;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    public GameState State { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        // start in idle, show message screen, freeze bird
        State = GameState.Idle;
        messagePanel.SetActive(true);
        gameOverPanel.SetActive(false);
        birdController.ResetBird();
    }

    public void StartGame()
    {
        State = GameState.Playing;
        messagePanel.SetActive(false);
        birdController.StartPlaying();
        pipeSpawner.StartSpawning();
    }

    public void OnBirdDied()
    {
        if (State == GameState.Dead)
        {
            return;
        }

        State = GameState.Dead;
        pipeSpawner.StopSpawning();
        cameraShake.Shake();

        ScoreManager.Instance.UpdateHighScore();

        finalScoreText.text = "Score: " + ScoreManager.Instance.GetScore().ToString();
        highScoreText.text = "Best: " + ScoreManager.Instance.GetHighScore().ToString();

        // freeze time so game over panel feels clean
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // destroy leftover pipes from last run
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");

        foreach (GameObject pipe in pipes)
        {
            Destroy(pipe);
        }

        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
        ScoreManager.Instance.ResetScore();
        birdController.ResetBird();
        State = GameState.Idle;
        messagePanel.SetActive(true);
    }
}
