using UnityEngine;

public class LetterBox : MonoBehaviour
{
    private string correctObject;
    public GameManager gameManager;

    private bool itemProcessed = false;
    private ThrowableItem lastProcessedItem;

    public void SetCorrectObject(string name)
    {
        correctObject = name;
        itemProcessed = false;
        lastProcessedItem = null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (itemProcessed) return;

        ThrowableItem item = other.GetComponentInParent<ThrowableItem>();
        if (item == null) return;

        if (lastProcessedItem == item) return;

        lastProcessedItem = item;
        itemProcessed = true;

        Debug.Log("В коробке предмет: " + item.objectName + " | нужно: " + correctObject);

        if (item.objectName == correctObject)
        {
            gameManager.CorrectAnswer();
        }
        else
        {
            gameManager.WrongAnswer();
        }

        Destroy(item.gameObject);
    }
}