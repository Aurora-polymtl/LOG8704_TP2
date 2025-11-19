using Mediapipe.Tasks.Vision.HandLandmarker;
using Mediapipe.Unity;
using Mediapipe.Unity.Sample.HandLandmarkDetection;
using UnityEngine;

public class FistDetector : MonoBehaviour
{
    [SerializeField] private MultiHandLandmarkListAnnotation landmarkAnnotation;
    [SerializeField] private HandLandmarkerResultAnnotationController annotationController;

    [Header("Interaction Main")]
    [SerializeField] private float closedThreshold = 0.1f;

    [Header("Event Channel")]
    [SerializeField] private FistStateChannel fistStateChannel; 

    private HandLandmarkerResult latestResult;
    private bool hasResult = false;
    private HandState currentState = HandState.None;

    public void OnHandLandmarkResult(HandLandmarkerResult result)
    {
        latestResult = result;
        hasResult = true;
    }

    void Update()
    {
        HandState newState;
        if (!hasResult || latestResult.handLandmarks == null || latestResult.handLandmarks.Count == 0)
        {
            newState = HandState.None;
            if (newState != currentState)
            {
                currentState = newState;
                fistStateChannel?.RaiseEvent(newState);
                Debug.Log($"Nouvel état de main : {newState}");
            }
            return;
        }

            var handLandmarks = latestResult.handLandmarks[0].landmarks;
        int fingersClosed = 0;
        int[] tipIndices = { 8, 12, 16, 20 };

        foreach (int i in tipIndices)
        {
            var tip = handLandmarks[i];
            var wrist = handLandmarks[0];
            float dist = Vector3.Distance(
                new Vector3(tip.x, tip.y, tip.z),
                new Vector3(wrist.x, wrist.y, wrist.z)
            );

            if (dist < closedThreshold)
                fingersClosed++;
        }

        if (fingersClosed == 4)
            newState = HandState.Closed;
        else if (fingersClosed == 0)
            newState = HandState.Open;
        else
            newState = HandState.Partial;

        // On ne déclenche que si l’état change
        if (newState != currentState)
        {
            currentState = newState;
            fistStateChannel?.RaiseEvent(newState);
            Debug.Log($"Nouvel état de main : {newState}");
        }

        // Optionnel : coloration des landmarks
        //Color color = newState switch
        //{
        //    HandState.Closed => Color.red,
        //    HandState.Open => Color.green,
        //    _ => Color.yellow
        //};

        //landmarkAnnotation.SetRightLandmarkColor(color);
        //landmarkAnnotation.SetLeftLandmarkColor(color);
        //annotationController.DrawNow(latestResult);
    }
}
