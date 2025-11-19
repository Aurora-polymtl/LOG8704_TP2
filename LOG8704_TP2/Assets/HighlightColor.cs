using UnityEngine;
using UnityEngine.UI;

public class HighlightColor : MonoBehaviour
{
    private Color baseColor;
    private Image image;
    private MeshRenderer renderer3D;

    private float originalY;

    [Header("Highlight Settings")]
    public Color highlightColor = Color.yellow;
    public float liftAmount = 0.1f;       // en pixels si UI, ou units si 3D
    public float liftSpeed = 10f;

    private bool isHighlighted = false;

    void Awake()
    {
        image = GetComponent<Image>();
        renderer3D = GetComponent<MeshRenderer>();

        originalY = transform.localPosition.y;

        if (image != null)
            baseColor = image.color;
        else if (renderer3D != null)
            baseColor = renderer3D.material.color;
    }

    void Update()
    {
        float targetY = originalY + (isHighlighted ? liftAmount : 0f);
        Vector3 pos = transform.localPosition;
        pos.y = Mathf.Lerp(pos.y, targetY, Time.deltaTime * liftSpeed);
        transform.localPosition = pos; // seulement Y
    }

    public void SetHighlight(bool active)
    {
        isHighlighted = active;

        if (image != null)
            image.color = active ? highlightColor : baseColor;

        if (renderer3D != null)
            renderer3D.material.color = active ? highlightColor : baseColor;
    }
}
