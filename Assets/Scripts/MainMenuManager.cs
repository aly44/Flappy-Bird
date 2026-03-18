using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    private const string HIGH_SCORE_KEY = "HS_FlappyBird";

    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Start()
    {
        int highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        highScoreText.text = "Best: " + highScore.ToString();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
}
