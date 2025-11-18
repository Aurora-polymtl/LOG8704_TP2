using UnityEngine;

public class ScalingHighlight : MonoBehaviour
{
    public float highlightScale = 1.2f;
    public float speed = 10f;

    private Vector3 baseScale;
    private Vector3 targetScale;

    void Awake()
    {
        baseScale = transform.localScale;
        targetScale = baseScale;
    }

    public void Highlight(bool active)
    {
        targetScale = active ? baseScale * highlightScale : baseScale;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
    }
}
