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
    public float minSpacing = 0.3f;

    private List<GameObject> spawnedWaste = new List<GameObject>();
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool spawned = false;

    void Update()
    {
        if (spawned) return;

        // On attend un plan AR valide (au centre de l'écran)
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        if (raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            SpawnWasteOnLargeArea(hitPose.position);
            spawned = true;
        }
    }

    void SpawnWasteOnLargeArea(Vector3 center)
    {
        int count = 0;
        int safetyLimit = 500; // évite boucle infinie

        while (count < numberOfWaste && safetyLimit-- > 0)
        {
            // Position aléatoire dans un disque de rayon spawnRadius
            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
            Vector3 randomPos = center + new Vector3(randomCircle.x, 0, randomCircle.y);

            // Vérifie l'espacement minimal entre les objets
            bool tooClose = false;
            foreach (var obj in spawnedWaste)
            {
                if (Vector3.Distance(obj.transform.position, randomPos) < minSpacing)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
            {
                var prefab = wastePrefabs[Random.Range(0, wastePrefabs.Count)];
                var waste = Instantiate(prefab, randomPos, Quaternion.Euler(0, Random.Range(0, 360), 0));
                spawnedWaste.Add(waste);
                count++;
            }
        }
        WasteManager.Instance.RegisterInitialWaste(numberOfWaste);
    }
}
