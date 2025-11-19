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

    public UIMessageManager uiMessageManager;
    public ScoreManager scoreManager;
    public BinUIManager binUIManager;

    void Awake()
    {
        if (arCamera == null)
            arCamera = Camera.main;
    }

    void Start()
    {
        // Trouve automatiquement le UI manager
        if (uiMessageManager == null)
            uiMessageManager = FindFirstObjectByType<UIMessageManager>();
        if (scoreManager == null)
            scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    void Update()
    {
        Vector2 pointerPos = GetPointerPosition();
        if (pointerPos == Vector2.negativeInfinity)
            return;

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
                TryGrab(pointerPos);

            if (Mouse.current.leftButton.isPressed)
                DragObject(pointerPos);

            if (Mouse.current.leftButton.wasReleasedThisFrame)
                Release();
        }
#else
    // Mobile touch
    if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0)
    {
        var touch = Touchscreen.current.touches[0];
        switch (touch.phase.ReadValue())
        {
            case UnityEngine.InputSystem.TouchPhase.Began:
                TryGrab(pointerPos);
                break;

            case UnityEngine.InputSystem.TouchPhase.Moved:
            case UnityEngine.InputSystem.TouchPhase.Stationary:
                DragObject(pointerPos);
                break;

            case UnityEngine.InputSystem.TouchPhase.Ended:
            case UnityEngine.InputSystem.TouchPhase.Canceled:
                Release();
                break;
        }
    }
#endif
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
            
            if (binUIManager != null)
                binUIManager.SetBinsVisible(true);
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
        if (item == null) { grabbedObject = null; return; }

        TrashBin touchedBin = CheckDropZone(item);

        if (touchedBin != null)
        {
            if (item.wasteType == touchedBin.binType)
            {
                uiMessageManager.ShowCorrect();
                scoreManager.AddPoint();
                Debug.Log("CORRECT : " + item.name + " -> " + touchedBin.binType);
                
            }
            else
            {
                uiMessageManager.ShowWrong();
                Debug.Log("MAUVAISE POUBELLE pour " + item.name);                
            }
            Destroy(item.gameObject);
            WasteManager.Instance.WasteRemoved();
        }
        else
        {
            Debug.Log("Déchet relâché, mais pas sur une poubelle.");
        }

        if (binUIManager != null)
        binUIManager.SetBinsVisible(false);

        grabbedObject = null;
    }


    // --------------------------
    // Input system
    // --------------------------
    Vector2 GetPointerPosition()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Mouse.current != null)
            return Mouse.current.position.ReadValue();
#endif

        if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0)
            return Touchscreen.current.touches[0].position.ReadValue();

        return Vector2.negativeInfinity;
    }


    public TrashBin CheckDropZone(WasteItem item)
    {
        TrashBin[] bins = FindObjectsByType<TrashBin>(FindObjectsSortMode.None);
        foreach (TrashBin bin in bins)
        {
            Collider c = bin.GetComponent<Collider>();
            if (c == null) continue;

            // On construit une OverlapBox centrée sur la poubelle
            Collider[] hits = Physics.OverlapBox(
                c.bounds.center,         // centre de la zone
                c.bounds.extents,        // demi-dimensions
                bin.transform.rotation   // orientation
            );

            // On vérifie si le déchet se trouve parmi les hits
            foreach (Collider hit in hits)
            {
                if (hit.gameObject == item.gameObject)
                    return bin; // trouvé !
            }
        }

        return null; // aucun bin touché
    }

}
