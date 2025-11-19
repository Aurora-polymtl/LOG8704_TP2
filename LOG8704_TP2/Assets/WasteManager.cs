using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WasteManager : MonoBehaviour
{
    public static WasteManager Instance;

    public int activeWasteCount = 0;
    public GameObject completionParticlesPrefab;
    public string menuSceneName = "Menu";

    private void Awake()
    {
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
        activeWasteCount--;

        if (activeWasteCount <= 0)
        {
            TriggerCompletionEffect();
            StartCoroutine(ReturnToMenuAfterDelay(30f));
        }
    }

    private void TriggerCompletionEffect()
    {
        if (completionParticlesPrefab != null)
        {
            Transform cam = Camera.main.transform;
            Vector3 pos = cam.position + cam.forward * 2.0f;

            Instantiate(completionParticlesPrefab, pos, Quaternion.identity);
        }
    }

    private IEnumerator ReturnToMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(menuSceneName);
    }
}
