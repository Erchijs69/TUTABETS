using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LetterData[] letters;
    public ObjectSpawner spawner;
    public LetterBox letterBox;
    public LetterDisplay display;
    public float nextRoundDelay = 1.5f;

    private int currentIndex = 0;
    private bool roundFinished = false;
    private LetterData currentLetter;

    void Start()
    {
        if (letters == null || letters.Length == 0)
        {
            Debug.LogError("Letters array is empty!");
            return;
        }

        currentIndex = 0;
        StartRound();
    }

    public void StartRound()
    {
        if (currentIndex >= letters.Length)
        {
            Debug.Log("All letters completed!");
            display.ShowFinished();
            return;
        }

        roundFinished = false;
        currentLetter = letters[currentIndex];

        display.ShowLetter(currentLetter);

        spawner.SpawnObjects(
            currentLetter.correctObject,
            currentLetter.wrongObjects
        );

        letterBox.SetCorrectObject(currentLetter.correctObject.name);
    }

    public void CorrectAnswer()
    {
        if (roundFinished) return;

        roundFinished = true;
        Debug.Log("Correct! " + currentLetter.letter);

        currentIndex++;
        Invoke(nameof(StartRound), nextRoundDelay);
    }

    public void WrongAnswer()
    {
        if (roundFinished) return;

        Debug.Log("Wrong! " + currentLetter.letter);
    }
}