using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class WasteSpawner : MonoBehaviour
{
    [Header("AR Components")]
    public ARRaycastManager raycastManager;

    [Header("Prefabs de déchets")]
    public List<GameObject> wastePrefabs;

    [Header("Paramètres de génération")]
    [Tooltip("Nombre total de déchets à générer")]
    public int numberOfWaste = 20;

    [Tooltip("Rayon en mètres autour du point central où les déchets peuvent apparaître")]
    public float spawnRadius = 2.5f;

    [Tooltip("Distance minimale entre chaque déchet")]
    public float minSpacing = 0.1f;

    private List<GameObject> spawnedWaste = new List<GameObject>();
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool spawned = false;

    public float spawnDelay = 5f; // attendre 5 secondes avant de spawn

    private float timer = 0f;

    public GameObject loadingIndicator;


    void Update()
    {
        if (spawned) return;

        timer += Time.deltaTime;

        if (loadingIndicator != null && !loadingIndicator.activeSelf)
            loadingIndicator.SetActive(true);

        // On attend le délai et qu'au moins un plan soit détecté
        if (timer >= spawnDelay && raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon))
        {
            SpawnWasteOnVisibleSurfaces();
            spawned = true;
            if (loadingIndicator != null)
                loadingIndicator.SetActive(false);

        }
    }

    void SpawnWasteOnVisibleSurfaces()
    {
        int count = 0;
        int maxAttempts = numberOfWaste * 30; // sécurité anti boucle infinie

        while (count < numberOfWaste && maxAttempts-- > 0)
        {
            // 1) Point aléatoire sur l’écran, mais pas trop au bord
            Vector2 randomScreenPos = new Vector2(
                Random.Range(Screen.width * 0.15f, Screen.width * 0.85f),
                Random.Range(Screen.height * 0.15f, Screen.height * 0.85f)
            );

            // 2) AR Raycast depuis la caméra
            if (raycastManager.Raycast(randomScreenPos, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                // 3) Vérifier distance minimale entre déchets
                bool tooClose = false;
                foreach (var w in spawnedWaste)
                {
                    if (Vector3.Distance(w.transform.position, hitPose.position) < minSpacing)
                    {
                        tooClose = true;
                        break;
                    }
                }
                if (tooClose) continue;

                // 4) Instanciation du déchet
                GameObject prefab = wastePrefabs[Random.Range(0, wastePrefabs.Count)];
                GameObject waste = Instantiate(
                    prefab,
                    hitPose.position,
                    Quaternion.Euler(0, Random.Range(0, 360), 0)
                );

                spawnedWaste.Add(waste);
                count++;
            }
        }

        WasteManager.Instance.RegisterInitialWaste(spawnedWaste.Count);
    }


}
