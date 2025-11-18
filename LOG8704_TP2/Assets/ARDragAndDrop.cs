using UnityEngine;
using UnityEngine.InputSystem;

public class ARDragAndDrop : MonoBehaviour
{
    public Camera arCamera;
    public float maxDistance = 5f;
    public float dragDistance = 1.5f;   // distance entre caméra et objet quand on le tient
    public LayerMask draggableLayer;

    public float dragDistanceMin = 0.5f;   // distance minimum (quand pointeur en bas)
    public float dragDistanceMax = 3.5f;   // distance maximum (quand pointeur en haut)
    public float heightOffset = 0.01f;

    private Transform grabbedObject = null;
    private Rigidbody grabbedRb = null;

    void Awake()
    {
        if (arCamera == null)
            arCamera = Camera.main;
    }

    void Update()
    {
        Vector2 pointerPos = GetPointerPosition();
        if (pointerPos == Vector2.negativeInfinity)
            return;

        if (Mouse.current != null)
        {
            // CLIC POUR PRENDRE
            if (Mouse.current.leftButton.wasPressedThisFrame)
                TryGrab(pointerPos);

            // MAINTIEN POUR DÉPLACER
            if (Mouse.current.leftButton.isPressed)
                DragObject(pointerPos);

            // RELÂCHE POUR LÂCHER
            if (Mouse.current.leftButton.wasReleasedThisFrame)
                Release();
        }
    }

    // --------------------------
    // Grab
    // --------------------------
    void TryGrab(Vector2 screenPos)
    {
        Ray ray = arCamera.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, draggableLayer))
        {
            grabbedObject = hit.collider.transform;
            grabbedRb = grabbedObject.GetComponent<Rigidbody>();

            if (grabbedRb != null)
                grabbedRb.isKinematic = true;  // empêche la physique pendant le drag
        }
    }

    // --------------------------
    // Drag
    // --------------------------
    void DragObject(Vector2 screenPos)
    {
        if (grabbedObject == null) return;

        // calcul de la proportion verticale (0 = bas, 1 = haut)
        float verticalNorm = Mathf.Clamp01(screenPos.y / Screen.height);

        // interpolation linéaire : plus le pointeur est bas, plus on rapproche
        float dragDistance = Mathf.Lerp(dragDistanceMin, dragDistanceMax, verticalNorm);

        // rayon depuis la caméra
        Ray ray = arCamera.ScreenPointToRay(screenPos);

        // position cible devant la caméra
        Vector3 targetPos = ray.origin + ray.direction * dragDistance;

        // offset vertical pour passer au-dessus des poubelles
        //targetPos.y += heightOffset;

        // smooth follow (optionnel)
        grabbedObject.position = Vector3.Lerp(grabbedObject.position, targetPos, Time.deltaTime * 10f);
    }

    // --------------------------
    // Release
    // --------------------------
    void Release()
    {
        if (grabbedObject == null) return;

        WasteItem item = grabbedObject.GetComponent<WasteItem>();
        if (item != null)
        {
            // On cherche toutes les poubelles
            TrashBin[] bins = FindObjectsOfType<TrashBin>();
            foreach (TrashBin bin in bins)
            {
                if (bin.IsWasteOverlapping(item))
                {
                    if (item.wasteType == bin.binType)
                    {
                        Debug.Log(item.name + " est correctement déposé dans " + bin.binType);
                        // ici tu peux faire score, effet visuel, destroy item etc.
                    }
                    else
                    {
                        Debug.Log(item.name + " est dans la mauvaise poubelle !");
                        // feedback visuel ou sonore
                    }

                    break; // on ne vérifie qu’une poubelle
                }
            }
        }

        grabbedObject = null; // on lâche le déchet
    }

    // --------------------------
    // Input system
    // --------------------------
    Vector2 GetPointerPosition()
    {
        if (Mouse.current != null)
            return Mouse.current.position.ReadValue();

        if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0)
            return Touchscreen.current.touches[0].position.ReadValue();

        return Vector2.negativeInfinity;
    }
}
