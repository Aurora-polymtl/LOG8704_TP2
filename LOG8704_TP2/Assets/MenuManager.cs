using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button startButton;
    public string gameSceneName = "GameScene"; // Nom de ta scène de jeu
    public GameObject menuPanel;
    public GameObject optionsPanel;

    void Start()
    {
        if (startButton != null)
            startButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenOptions()
    {
        menuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        menuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }
}
