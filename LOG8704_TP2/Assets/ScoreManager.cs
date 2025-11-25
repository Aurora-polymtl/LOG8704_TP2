using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
    public Text scoreText;

    void Awake()
    {
        Instance = this;
    }

    public void AddPoint()
    {
        score++;
        UpdateScoreUI();
    }

    public int GetScore()
    {
        return score;
    }

    public void hideScore()
    {
        if (scoreText != null)
            scoreText.gameObject.SetActive(false);
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Points : " + score;
    }


}
