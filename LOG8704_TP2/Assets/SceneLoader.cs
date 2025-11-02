using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Appelé depuis le bouton
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // (Optionnel) Pour quitter l’application
    public void QuitGame()
    {
        Application.Quit();
    }
}