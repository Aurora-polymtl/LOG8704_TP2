using UnityEngine;
using UnityEngine.UI;

public class HighlightColor : MonoBehaviour
{
    private Color baseColor;
    private Image image;
    private MeshRenderer renderer3D;

    public Color highlightColor = Color.yellow;

    void Awake()
    {
        image = GetComponent<Image>();
        renderer3D = GetComponent<MeshRenderer>();

        if (image != null)
            baseColor = image.color;
        else if (renderer3D != null)
            baseColor = renderer3D.material.color;
    }

    public void SetHighlight(bool active)
    {
        if (image != null)
            image.color = active ? highlightColor : baseColor;

        if (renderer3D != null)
            renderer3D.material.color = active ? highlightColor : baseColor;
    }
}
