using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LetterData[] letters;
    public ObjectSpawner spawner;
    public LetterBox letterBox;
    public LetterDisplay display;

    private int currentIndex = 0;
    private bool roundFinished = false;
    private LetterData currentLetter;

    void Start()
    {
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
        Debug.Log("Correct!");

        currentIndex++;
        Invoke(nameof(StartRound), 0.5f);
    }

    public void WrongAnswer()
    {
        if (roundFinished) return;

        Debug.Log("Wrong!");
    }
}