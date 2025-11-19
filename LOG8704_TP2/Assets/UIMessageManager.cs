using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMessageManager : MonoBehaviour
{
    public Text messageText;
    public float messageDuration = 2f;

    public void ShowCorrect()
    {
        StopAllCoroutines();
        StartCoroutine(ShowMessage("Bonne poubelle!", Color.green));
    }

    public void ShowWrong()
    {
        StopAllCoroutines();
        StartCoroutine(ShowMessage("Mauvaise poubelle!", Color.red));
    }

    private IEnumerator ShowMessage(string text, Color color)
    {
        messageText.text = text;
        messageText.color = color;
        messageText.gameObject.SetActive(true);

        yield return new WaitForSeconds(messageDuration);

        messageText.gameObject.SetActive(false);
    }
}
