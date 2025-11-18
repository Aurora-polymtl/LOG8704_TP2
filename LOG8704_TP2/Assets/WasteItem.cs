using UnityEngine;

public enum WasteType
{
    General,    // poubelle classique
    Recyclable, // recyclage
    Compost     // compost
}


public class WasteItem : MonoBehaviour
{

    public WasteType wasteType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
