using UnityEngine;
using UnityEngine.UI;

public class HandIndicatorUI : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private FistStateChannel channel;
    [SerializeField] private Image feedbackImage;

    void OnEnable()
    {
        channel.OnEventRaised += ShowFeedback;
    }

    void OnDisable()
    {
        channel.OnEventRaised -= ShowFeedback;
    }

    void Start()
    {
        if (feedbackImage != null)
            feedbackImage.enabled = false; // caché au démarrage
    }

    private void ShowFeedback(HandState state)
    {
        if (feedbackImage == null)
            return;

        // Couleur selon l'état
        switch (state)
        {
            case HandState.Closed:
                feedbackImage.color = Color.red;
                break;
            case HandState.Open:
                feedbackImage.color = Color.green;
                break;
            case HandState.Partial:
                feedbackImage.color = Color.yellow;
                break;
            case HandState.None:
                feedbackImage.color = Color.white;
                break;
        }

        feedbackImage.enabled = true; // reste affiché en permanence
    }
}
