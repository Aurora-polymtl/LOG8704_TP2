using Mediapipe.Tasks.Vision.HandLandmarker;
using Mediapipe.Unity;
using Mediapipe.Unity.Sample.HandLandmarkDetection;
using UnityEngine;

public class FistDetector : MonoBehaviour
{
    [SerializeField] private MultiHandLandmarkListAnnotation landmarkAnnotation; // Référence dans l’inspecteur
    [SerializeField] private HandLandmarkerResultAnnotationController annotationController;

    private HandLandmarkerResult latestResult;
    private bool hasResult = false;
    [SerializeField] private float closedThreshold = 0.1f;

    public void OnHandLandmarkResult(HandLandmarkerResult result)
    {
        latestResult = result;
        hasResult = true;
    }

    void Update()
    {
        if (!hasResult || latestResult.handLandmarks == null || latestResult.handLandmarks.Count == 0)
            return;

        var handLandmarks = latestResult.handLandmarks[0].landmarks;
        int fingersClosed = 0;
        int[] tipIndices = { 8, 12, 16, 20 };

        foreach (int i in tipIndices)
        {
            var tip = handLandmarks[i];
            var wrist = handLandmarks[0];
            float dist = Vector3.Distance(new Vector3(tip.x, tip.y, tip.z),
                                          new Vector3(wrist.x, wrist.y, wrist.z));

            if (dist < closedThreshold)
                fingersClosed++;
        }

        if (fingersClosed == 4)
        {
            Debug.Log("Poing fermé !");
            landmarkAnnotation.SetRightLandmarkColor(Color.red);
            landmarkAnnotation.SetLeftLandmarkColor(Color.red);
        }
        else if (fingersClosed == 0)
        {
            Debug.Log("Poing ouvert !");
            landmarkAnnotation.SetRightLandmarkColor(Color.green);
            landmarkAnnotation.SetLeftLandmarkColor(Color.green);
        }
        else
        {
            Debug.Log("Main partiellement fermée");
            landmarkAnnotation.SetRightLandmarkColor(Color.yellow);
            landmarkAnnotation.SetLeftLandmarkColor(Color.yellow);
        }

        annotationController.DrawNow(latestResult);

    }
}
