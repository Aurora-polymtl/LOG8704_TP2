using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button startButton;
    public string gameSceneName = "GameScene"; // Nom de ta scène de jeu

    void Start()
    {
        if (startButton != null)
            startButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
