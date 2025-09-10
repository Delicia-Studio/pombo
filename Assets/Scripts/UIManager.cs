using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Text scoreText;
    public Text breadText;
    public GameObject gameOverPanel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateBread(int bread)
    {
        breadText.text = "Bread: " + bread;
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
