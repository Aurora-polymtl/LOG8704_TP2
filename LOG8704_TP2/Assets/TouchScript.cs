using UnityEngine;
using UnityEngine.InputSystem; // Nouveau système d’entrée

public class TouchScript : MonoBehaviour
{
    private Camera arCamera;

    void Start()
    {
        // Trouver la caméra AR active
        arCamera = Camera.main;
        if (arCamera == null)
        {
            arCamera = FindAnyObjectByType<Camera>();
        }
    }

    void Update()
    {
        Vector2? touchPosition = null;

#if UNITY_EDITOR
        // Mode ÉDITEUR : clic de souris
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            touchPosition = Mouse.current.position.ReadValue();
        }
#else
        // Mode APPAREIL MOBILE : toucher écran
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        }
#endif

        // Si on a une position valide (clic ou toucher)
        if (touchPosition.HasValue)
        {
            Ray ray = arCamera.ScreenPointToRay(touchPosition.Value);
            // Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 0.5f);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                Debug.Log($"Ray hit: {hit.transform.name}");

                if (hit.transform.CompareTag("TouchableCube"))
                {
                    Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
                    }
                }
            }
            else
            {
                Debug.Log("Ray missed everything.");
            }
        }
    }
}
