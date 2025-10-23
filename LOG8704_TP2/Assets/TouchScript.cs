using UnityEngine;
using UnityEngine.InputSystem;

public class TouchScript : MonoBehaviour
{
    private Camera arCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        arCamera = Camera.main;
        if (arCamera == null)
        {
            arCamera = FindAnyObjectByType<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Pour l'editeur Unity, on utilise le clic gauche de la souris. Commentez cette ligne pour les appareils tactiles
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        // Pour les appareils tactiles, décommentez cette ligne
        // if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            // Décommenter cette ligne pour les appareils tactiles
            // Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            // Pour l'editeur Unity, on utilise la position de la souris. Commentez cette ligne pour les appareils tactiles
            Vector2 touchPosition = Mouse.current.position.ReadValue();
            
            Debug.Log("Touch detected at: " + touchPosition);
            Ray ray = arCamera.ScreenPointToRay(touchPosition);
            RaycastHit hit;
            //Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 1f);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                Debug.Log("Touched object: " + hit.transform.name);
                if (hit.transform.CompareTag("TouchableCube"))
                {
                    Debug.Log("Cube touched!");
                    Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
                    }
                }
            }
        }
    }
}
