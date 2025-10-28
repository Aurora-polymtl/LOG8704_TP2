using UnityEngine;
using Mediapipe.Tasks.Vision.HandLandmarker;
using System.Collections.Generic;

namespace Mediapipe.Unity.Sample.HandLandmarkDetection
{
    public class FistDetector : MonoBehaviour
    {
        // Stocke le dernier résultat
        private HandLandmarkerResult latestResult;
        private bool hasResult = false; // vrai si un résultat a été reçu

        // Seuil pour considérer un doigt comme fermé (à ajuster)
        [SerializeField] private float closedThreshold = 0.1f;

        // Appelé par HandLandmarkerRunner
        public void OnHandLandmarkResult(HandLandmarkerResult result)
        {
            latestResult = result;
            hasResult = true;
        }

        void Update()
        {
            if (!hasResult) return; // aucun résultat reçu

            // Vérifier que la main existe
            if (latestResult.handLandmarks == null || latestResult.handLandmarks.Count == 0)
                return;

            var handLandmarks = latestResult.handLandmarks[0].landmarks; // RepeatedField<NormalizedLandmark>

            if (handLandmarks == null || handLandmarks.Count == 0)
                return;

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

                if (dist < closedThreshold) fingersClosed++;
            }

            if (fingersClosed == 4)
                Debug.Log("Poing fermé !");
            else if (fingersClosed == 0)
                Debug.Log("Poing ouvert !");
            else
                Debug.Log("Main partiellement fermée");
        }



    }
}
