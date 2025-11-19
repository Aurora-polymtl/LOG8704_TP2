using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
    public Text statusText;

    void Update()
    {
        // Appel d'une fonction qui renvoie une valeur
        float value = GetValueFromSensor();

        // Changer le texte selon la valeur
        if (value > 0.5f)
            statusText.text = "Main détectée";
        else
            statusText.text = "Aucune main détectée";
    }

    float GetValueFromSensor()
    {
        // Exemple : valeur simulée entre 0 et 1
        return Mathf.PingPong(Time.time * 0.3f, 1f);
    }
}
