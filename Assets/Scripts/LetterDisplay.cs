using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterDisplay : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI text;

    public void ShowLetter(LetterData data)
    {
        if (image != null)
        {
            image.sprite = data.letterSprite;
            image.enabled = data.letterSprite != null;
            image.type = Image.Type.Simple;
            image.preserveAspect = false;
        }

        if (text != null)
        {
            text.text = data.letter;
        }
    }

    public void ShowFinished()
    {
        if (image != null)
            image.enabled = false;

        if (text != null)
            text.text = "Pabeigts";
    }
}