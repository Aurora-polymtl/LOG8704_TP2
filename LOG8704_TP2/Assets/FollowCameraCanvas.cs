using UnityEngine;

public class FollowCameraCanvas : MonoBehaviour
{
    [Header("Références")]
    public Camera arCamera;

    [Header("Paramètres")]
    [Tooltip("Distance à laquelle le Canvas reste devant la caméra (en mètres)")]
    public float distance = 1.0f;

    [Tooltip("Vitesse de suivi (1 = instantané, <1 = plus doux)")]
    [Range(0.0f, 1.0f)]
    public float followSpeed = 1.0f;

    void LateUpdate()
    {
        if (arCamera == null)
            arCamera = Camera.main;

        // Position souhaitée devant la caméra
        Vector3 targetPos = arCamera.transform.position + arCamera.transform.forward * distance;

        // Mouvement fluide vers la position cible
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed);

        // Toujours orienté face à la caméra
        Quaternion targetRot = Quaternion.LookRotation(transform.position - arCamera.transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, followSpeed);
    }
}
