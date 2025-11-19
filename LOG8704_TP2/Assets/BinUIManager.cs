using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BinUIManager : MonoBehaviour
{
    [Header("Références")]
    public Transform binContainer; // ton conteneur dans le Canvas
    public GameObject wasteBinPrefab;     // Déchets
    public GameObject recycleBinPrefab;   // Recyclage
    public GameObject compostBinPrefab;   // Compost

    [Header("Taille des bacs (dans le Canvas)")]
    public Vector2 binSize = new Vector2(150, 150); // pixels

    [HideInInspector]
    public List<GameObject> bins = new List<GameObject>();

    void Start()
    {
        // Vérifie la présence du conteneur
        if (binContainer == null)
        {
            Debug.LogError("Aucun conteneur de bacs assigné !");
            return;
        }

        // Crée et place les bacs
        bins.Add(CreateBinUI(wasteBinPrefab, "Déchets"));
        bins.Add(CreateBinUI(recycleBinPrefab, "Recyclage"));
        bins.Add(CreateBinUI(compostBinPrefab, "Compost"));

        SetBinsVisible(false);
    }

    GameObject CreateBinUI(GameObject prefab, string name)
    {
        if (prefab == null) return null;

        GameObject binUI = Instantiate(prefab, binContainer);
        binUI.name = name;

        // Ajuste la taille dans le Canvas
        RectTransform rect = binUI.GetComponent<RectTransform>();
        if (rect == null)
            rect = binUI.AddComponent<RectTransform>();

        rect.sizeDelta = binSize;
        return binUI;
    }

    public void SetBinsVisible(bool visible)
    {
        foreach (var bin in bins)
        {
            if (bin != null)
            {
                // Remet la couleur de base avant de cacher
                HighlightColor highlight = bin.GetComponent<HighlightColor>();
                if (highlight != null)
                    highlight.SetHighlight(false);

                bin.SetActive(visible);
            }
        }
    }
}
