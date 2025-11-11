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

    void Start()
    {
        // Vérifie la présence du conteneur
        if (binContainer == null)
        {
            Debug.LogError("Aucun conteneur de bacs assigné !");
            return;
        }

        // Crée et place les bacs
        CreateBinUI(wasteBinPrefab, "Déchets");
        CreateBinUI(recycleBinPrefab, "Recyclage");
        CreateBinUI(compostBinPrefab, "Compost");
    }

    void CreateBinUI(GameObject prefab, string name)
    {
        if (prefab == null) return;

        GameObject binUI = Instantiate(prefab, binContainer);
        binUI.name = name;

        // Ajuste la taille dans le Canvas
        RectTransform rect = binUI.GetComponent<RectTransform>();
        if (rect == null)
            rect = binUI.AddComponent<RectTransform>();

        rect.sizeDelta = binSize;
    }
}
