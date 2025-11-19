using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public WasteType binType;

    // méthode pour vérifier si un déchet est dans la zone
    /*
    public bool IsWasteOverlapping(WasteItem item)
    {
        Collider binCollider = GetComponent<Collider>();
        Collider itemCollider = item.GetComponent<Collider>();

        if (binCollider == null || itemCollider == null) return false;

        // simple check : est-ce que le centre du déchet est dans le collider ?
        return binCollider.bounds.Contains(itemCollider.bounds.center);
    }*/
}
