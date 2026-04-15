using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LetterData[] letters;
    public ObjectSpawner spawner;
    public LetterBox letterBox;
    public LetterDisplay display;

    public float nextRoundDelay = 0.7f;

    private int currentIndex = 0;
    private bool roundFinished = false;
    private LetterData currentLetter;

    void Start()
    {
        if (letters == null || letters.Length == 0)
        {
            Debug.LogError("Массив letters пустой.");
            return;
        }

        currentIndex = 0;
        StartRound();
    }

    public void StartRound()
    {
        if (currentIndex >= letters.Length)
        {
            Debug.Log("Все буквы закончились.");
            display.ShowFinished();
            spawner.ClearObjects();
            return;
        }

        roundFinished = false;
        currentLetter = letters[currentIndex];

        display.ShowLetter(currentLetter);

        spawner.SpawnObjects(
            currentLetter.correctObject,
            letters
        );

        letterBox.SetCorrectItem(currentLetter.correctItemId);

        Debug.Log("Текущая буква: " + currentLetter.letter + " | правильный ID: " + currentLetter.correctItemId);
    }

    public void CorrectAnswer()
    {
        if (roundFinished) return;

        roundFinished = true;
        Debug.Log("Правильно!");

        currentIndex++;
        Invoke(nameof(StartRound), nextRoundDelay);
    }

    public void WrongAnswer()
    {
        if (roundFinished) return;

        Debug.Log("Неправильно! Буква остаётся прежней.");
    }
}