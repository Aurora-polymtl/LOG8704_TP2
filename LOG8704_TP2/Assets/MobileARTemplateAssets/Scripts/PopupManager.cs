using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MultiPagePopupManager : MonoBehaviour
{
    [SerializeField] private TouchScript touchScript;

    [Header("UI References")]
    public GameObject popupPanel;
    public RectTransform pagesContainer;      
    public List<GameObject> pages;         
    public Button nextButton;        
    public Button helpButton; 
    public Button deleteAllCubes;        


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
        if (helpButton != null) helpButton.onClick.AddListener(OpenPopup);

        // Popup initial
        popupPanel.SetActive(showPopupOnStart);
        helpButton.gameObject.SetActive(!showPopupOnStart);
        deleteAllCubes.gameObject.SetActive(!showPopupOnStart);

        // Initialise pages actives
        currentPage = Mathf.Clamp(startPageIndex, 0, pages.Count - 1);
        ShowPage(currentPage);
        if (touchScript != null)
            touchScript.enabled = false;
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
                    nextText.text = "Suivant >>";
            }
        }
    }

    public void OnNext()
    {
        if (currentPage < pages.Count - 1)
            ShowPage(currentPage + 1);
        else if (currentPage == pages.Count - 1)
        {
            popupPanel.SetActive(false);
            if (helpButton != null) helpButton.gameObject.SetActive(true);
            if (deleteAllCubes != null) deleteAllCubes.gameObject.SetActive(true);
            if (touchScript != null)
                touchScript.enabled = true;
        }

    }

    public void OpenPopup()
    {
        popupPanel.SetActive(true);
        if (helpButton != null) helpButton.gameObject.SetActive(false);
        if (deleteAllCubes != null) deleteAllCubes.gameObject.SetActive(false);
        ShowPage(startPageIndex);
        if (touchScript != null)
            touchScript.enabled = false;
    }
}
