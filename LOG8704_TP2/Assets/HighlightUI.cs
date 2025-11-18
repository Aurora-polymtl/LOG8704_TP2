using UnityEngine;
using UnityEngine.UI;

public class HighlightUI : MonoBehaviour
{
    public Image img;
    private Color baseColor;
    public Color highlightColor = Color.yellow;
    void Awake() { baseColor = img.color; }
    public void Highlight(bool active) { img.color = active ? highlightColor : baseColor; }
}
