using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGamePanel : MonoBehaviour
{
    public static EndGamePanel Instance;

    [Header("UI References")]
    public GameObject panelRoot;
    public TextMeshProUGUI scoreText;
    public Image awarenessImage;
    public TextMeshProUGUI awarenessText;
    public Button returnMenuButton;

    [Header("Contenu")]
    public Sprite awarenessSprite;
    [TextArea] public string messageDeSensibilisation;

    void Awake()
    {
        Instance = this;
        panelRoot.SetActive(false);
    }

    void Start()
    {
        returnMenuButton.onClick.AddListener(ReturnToMenu);
    }

    public void Show()
    {
        int finalScore = ScoreManager.Instance.GetScore();
        scoreText.text = "Pointage final : " + finalScore;

        awarenessImage.sprite = awarenessSprite;
        awarenessText.text = messageDeSensibilisation;

        panelRoot.SetActive(true);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
