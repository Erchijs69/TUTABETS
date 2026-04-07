using UnityEngine;
using UnityEngine.UI;

public class LetterDisplay : MonoBehaviour
{
    public Image image;

    public void ShowLetter(LetterData data)
    {
        image.sprite = data.letterSprite;
        image.enabled = true;
        image.type = Image.Type.Simple;
        image.preserveAspect = false;
    }

    public void ShowFinished()
    {
        image.enabled = false;
    }
}