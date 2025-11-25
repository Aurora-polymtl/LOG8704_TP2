using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WasteManager : MonoBehaviour
{
    public static WasteManager Instance;

    public int activeWasteCount = 0;
    public GameObject completionParticlesPrefab;
    public GameObject returnButton;

    public ScoreManager scoreManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }

    // Appelé par ton spawner
    public void RegisterInitialWaste(int count)
    {
        activeWasteCount = count;
    }

    // Appelé quand un déchet est détruit
    public void WasteRemoved()
    {
        if (SceneManager.GetActiveScene().name != "InteractScene")
            return;

        activeWasteCount--;

        if (activeWasteCount <= 0)
        {
            SFXManager.Instance.PlayEndGame();
            TriggerCompletionEffect();
            StartCoroutine(EndGameSequence());
        }
    }

    private void TriggerCompletionEffect()
    {
        if (SceneManager.GetActiveScene().name != "InteractScene")
            return;


        if (completionParticlesPrefab != null)
        {
            Camera camRef = GetMainCamera();
            if (camRef == null) return; // sécurité
            Transform cam = camRef.transform;
            Vector3 pos = cam.position + cam.forward * 2.0f;

            Instantiate(completionParticlesPrefab, pos, Quaternion.identity);
        }
    }

    private Camera GetMainCamera()
    {
        if (Camera.main != null)
            return Camera.main;

        // fallback si Camera.main n’est pas encore assignée
        return FindFirstObjectByType<Camera>();
    }


    IEnumerator EndGameSequence()
    {
        // 15 secondes pour laisser le WasteInfoPopup se fermer manuellement
        yield return new WaitForSeconds(15f);

        // On ferme le popup d'info s'il est encore ouvert
        if (WasteInfoPopupUI.Instance != null)
            WasteInfoPopupUI.Instance.ClosePopup();

        // Cache le vieux compteur de score
        if (scoreManager != null && scoreManager.scoreText != null)
            scoreManager.hideScore();
            Debug.Log("Hiding score text.");

        if (returnButton != null)
            returnButton.SetActive(false);

        // Affiche le nouveau panel
        EndGamePanel.Instance.Show();
    }

}
