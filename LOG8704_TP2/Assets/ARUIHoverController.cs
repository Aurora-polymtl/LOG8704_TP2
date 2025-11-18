using UnityEngine;
using UnityEngine.InputSystem;   // IMPORTANT

public class ARUIHoverController : MonoBehaviour
{
    public Camera arCamera;
    public float maxDistance = 5f;
    public bool debugMode = true;

    private GameObject currentObject = null;

    void Awake()
    {
        if (arCamera == null)
            arCamera = Camera.main;
    }

    void Update()
    {
        Vector2 screenPos = GetPointerPosition();
        if (screenPos == Vector2.negativeInfinity)
            return;  // aucun input dispo

        Ray ray = arCamera.ScreenPointToRay(screenPos);
        if (debugMode)
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            GameObject target = hit.collider.gameObject;

            if (target != currentObject)
            {
                // Enlever highlight de l'ancien
                if (currentObject != null)
                    SetHighlight(currentObject, false);

                currentObject = target;

                // Activer highlight
                SetHighlight(currentObject, true);

                if (debugMode)
                    Debug.Log("Hover: " + currentObject.name);
            }
        }
        else
        {
            if (currentObject != null)
                SetHighlight(currentObject, false);

            currentObject = null;
        }
    }

    // Méthode compatible Input System PC + Android
    Vector2 GetPointerPosition()
    {
        // 1) Souris (Unity Editor)
        if (Mouse.current != null)
            return Mouse.current.position.ReadValue();

        // 2) Écran tactile (Android)
        if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0)
            return Touchscreen.current.touches[0].position.ReadValue();

        // aucun input
        return Vector2.negativeInfinity;
    }

    void SetHighlight(GameObject obj, bool state)
    {
        var hc = obj.GetComponentInParent<HighlightColor>();
        if (hc != null)
        {
            hc.SetHighlight(state);
            return;
        }

        // fallback (si jamais un objet n'a pas HighlightColor)
        var img = obj.GetComponentInParent<UnityEngine.UI.Image>();
        if (img != null)
        {
            img.color = state ? Color.yellow : img.color; // laisse la couleur originale
            return;
        }

        var rend = obj.GetComponentInParent<MeshRenderer>();
        if (rend != null)
        {
            rend.material.color = state ? Color.yellow : rend.material.color;
        }
    }

}
