using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProjectPopupManager : MonoBehaviour
{ 
    [Header("UI References")]
    public GameObject popupPanel;
    public RectTransform pagesContainer;
    public List<GameObject> pages;
    public Button nextButton;
    public GameObject menuPanel;
    public GameObject backgroundObject;


    public int startPageIndex = 0;
    public bool showPopupOnStart = true;

    private int currentPage = 0;

    void Awake()
    {
        if (popupPanel == null)
            Debug.LogError("Missing popup panel");
        if (pagesContainer == null && pages == null)
            Debug.LogWarning("Missing pages container");

        // Hook boutons
        if (nextButton != null) nextButton.onClick.AddListener(OnNext);
        backgroundObject.SetActive(false);
    }


    void ShowPage(int pageIndex)
    {
        if (pages == null || pages.Count == 0) return;

        currentPage = Mathf.Clamp(pageIndex, 0, pages.Count - 1);

        for (int i = 0; i < pages.Count; i++)
            pages[i].SetActive(i == currentPage);

        if (nextButton != null)
        {
            TMP_Text nextText = nextButton.GetComponentInChildren<TMP_Text>();
            if (nextText != null)
            {
                if (currentPage >= pages.Count - 1)
                    nextText.text = "Terminer";
                else
                    nextText.text = "Suivant";
            }
        }
    }

    public void OnNext()
    {
        if (currentPage < pages.Count - 1)
        {
            currentPage++;
            ShowPage(currentPage);
        }
        else if (currentPage == pages.Count - 1)
        {
            popupPanel.SetActive(false);
            menuPanel.SetActive(true);

            backgroundObject.SetActive(false);
        }

    }

    public void OpenPopup()
    {
        currentPage = 0;
        popupPanel.SetActive(true);
        menuPanel.SetActive(false);

        backgroundObject.SetActive(true);
        ShowPage(startPageIndex);
    }
}
