using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WasteInfoPopupUI : MonoBehaviour
{
    public static WasteInfoPopupUI Instance;

    [Header("UI Elements")]
    public GameObject popupRoot;             // WasteInfoPopup
    public TMP_Text resultMessageText;       // "Bonne poubelle / Mauvaise poubelle"
    public TMP_Text infoText;                // Fait informatif
    public Image wasteImage;
    public Button closeButton;

    void Awake()
    {
        Instance = this;
        popupRoot.SetActive(false);
        closeButton.onClick.AddListener(ClosePopup);
    }

    public void ShowPopup(string resultMessage, string fact, Sprite image, bool correct)
    {
        resultMessageText.text = resultMessage;
        infoText.text = fact;

        resultMessageText.color = correct ? Color.green : Color.red;

        wasteImage.sprite = image;
        wasteImage.gameObject.SetActive(image != null);

        popupRoot.SetActive(true);
    }

    public void ClosePopup()
    {
        popupRoot.SetActive(false);
    }
}
