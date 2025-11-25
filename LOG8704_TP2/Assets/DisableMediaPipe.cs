using UnityEngine;

public class DisableMediaPipe : MonoBehaviour
{
    void Start()
    {
        var scripts = FindObjectsOfType<MonoBehaviour>();
        foreach (var s in scripts)
        {
            if (s.GetType().Namespace != null && s.GetType().Namespace.Contains("Mediapipe"))
            {
                s.enabled = false;
            }
        }
    }


// Update is called once per frame
    void Update()
    {
        
    }
}
