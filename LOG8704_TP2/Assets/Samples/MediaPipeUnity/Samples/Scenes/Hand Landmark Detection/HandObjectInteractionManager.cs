using System.Collections.Generic;
using Mediapipe.Tasks.Vision.HandLandmarker;
using UnityEngine;

public class HandObjectInteractionManager : MonoBehaviour
{
    [Header("References")]
    public Camera arCamera;                     // Camera AR utilisee dans la scene
    public List<Transform> interactiveObjects;  // Liste d'objets interactifs (3D)

    [Header("Coordonnees de la main (MediaPipe)")]
    public float handX;
    public float handY;

    [Header("Parametres d'interaction")]
    [Tooltip("Tolerance de superposition en coordonnees normalisees (0-1)")]
    public float interactionThreshold = 0.05f;

    private HandLandmarkerResult latestResult;
    private bool hasResult = false;

    public void OnHandLandmarkResult(HandLandmarkerResult result)
    {
        latestResult = result;
        hasResult = true;
        Vector2 palmCenter = GetPalmCenter2D(result);
        handX = palmCenter.x;
        handY = palmCenter.y;
    }

    public Vector2 GetPalmCenter2D(HandLandmarkerResult result, int handIndex = 0)
    {
        if (!hasResult || latestResult.handLandmarks == null || latestResult.handLandmarks.Count == 0)
            return Vector2.zero;

        var landmarks = latestResult.handLandmarks[0].landmarks;

        int[] palmIndices = { 0, 1, 5, 9, 13, 17 };
        float sumX = 0f, sumY = 0f;

        foreach (int i in palmIndices)
        {
            sumX += landmarks[i].x;
            sumY += landmarks[i].y;
        }

        float avgX = sumX / palmIndices.Length;
        float avgY = sumY / palmIndices.Length;

        Debug.Log($"coordonnées de la main : x = {avgX}, y = {avgY}");

        return new Vector2(avgX, avgY);
    }

    void Update()
    {
        if (arCamera == null || interactiveObjects == null || interactiveObjects.Count == 0)
            return;

        Vector2 handPos = new Vector2(handX, handY);

        foreach (Transform obj in interactiveObjects)
        {
            if (obj == null) continue;

            // 1 - Projeter la position de l'objet dans le plan 2D de la camera
            Vector3 screenPos = arCamera.WorldToScreenPoint(obj.position);

            // Si l'objet est derriere la camera, on saute
            if (screenPos.z < 0) continue;

            // 2 - Normaliser la position ecran en coordonnees [0,1]
            float normX = screenPos.x / Screen.width;
            float normY = 1f - (screenPos.y / Screen.height); // inversion axe Y

            Vector2 objectPos = new Vector2(normX, normY);

            // 3 - Calculer la distance entre la main et l'objet
            float distance = Vector2.Distance(handPos, objectPos);

            // 4 - Interaction detectee ?
            if (distance < interactionThreshold)
            {
                Debug.Log($"Interaction detectee avec {obj.name} (distance={distance:F3})");
                // Ici tu peux appeler une methode sur l'objet :
                // obj.GetComponent<MyInteractiveObject>()?.OnHandHover();
            }

            // 5 - (Optionnel) Visualisation Debug
            Debug.DrawLine(
                arCamera.ScreenToWorldPoint(new Vector3(handX * Screen.width, (1f - handY) * Screen.height, 1f)),
                obj.position,
                distance < interactionThreshold ? Color.green : Color.red
            );
        }
    }
}
