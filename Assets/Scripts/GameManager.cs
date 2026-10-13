
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Victory Screen")]
    public GameObject victoryPanel;
    public TMP_Text finalScoreText;

    private int score = 0;
    private bool gameEnded = false;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        victoryPanel.SetActive(false);
    }

    public void AddScore(int points)
    {
        if (!gameEnded)
        {
            score += points;
        }
    }

    public void LevelComplete()
    {
        if (gameEnded) return;

        gameEnded = true;

        finalScoreText.text = "FINAL SCORE: " + score;

        victoryPanel.SetActive(true);

        // Freeze Sonic, enemies, and gameplay.
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
