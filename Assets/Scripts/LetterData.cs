using UnityEngine;

[CreateAssetMenu(fileName = "LetterData", menuName = "Alphabet/Letter")]
public class LetterData : ScriptableObject
{
    public string letter;
    public Sprite letterSprite;

    [Header("Уникальный ID правильного предмета")]
    public string correctItemId;

    [Header("Правильный префаб")]
    public GameObject correctObject;
}