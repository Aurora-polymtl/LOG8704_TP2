using UnityEngine;

public class AskCameraPermission : MonoBehaviour
{
    void Start()
    {
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Application.RequestUserAuthorization(UserAuthorization.WebCam);
        }

        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
            Debug.Log("Permission caméra accordée !");
        else
            Debug.LogWarning("Permission caméra non encore accordée ou refusée.");
    }
}
