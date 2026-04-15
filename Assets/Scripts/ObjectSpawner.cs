using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    private readonly List<GameObject> spawnedObjects = new List<GameObject>();

    public void SpawnObjects(GameObject correctObject, LetterData[] allLetters)
    {
        ClearObjects();

        if (spawnPoints == null || spawnPoints.Length < 3)
        {
            Debug.LogError("Нужно минимум 3 spawn points.");
            return;
        }

        if (correctObject == null)
        {
            Debug.LogError("Correct object is null.");
            return;
        }

        List<GameObject> wrongPool = new List<GameObject>();

        for (int i = 0; i < allLetters.Length; i++)
        {
            if (allLetters[i] == null) continue;
            if (allLetters[i].correctObject == null) continue;

            if (allLetters[i].correctObject != correctObject)
            {
                wrongPool.Add(allLetters[i].correctObject);
            }
        }

        if (wrongPool.Count < 2)
        {
            Debug.LogError("Недостаточно объектов для выбора неправильных ответов.");
            return;
        }

        Shuffle(wrongPool);

        GameObject[] objectsToSpawn = new GameObject[3];
        objectsToSpawn[0] = correctObject;
        objectsToSpawn[1] = wrongPool[0];
        objectsToSpawn[2] = wrongPool[1];

        Shuffle(objectsToSpawn);

        for (int i = 0; i < 3; i++)
        {
            GameObject obj = Instantiate(
                objectsToSpawn[i],
                spawnPoints[i].position,
                spawnPoints[i].rotation
            );

            spawnedObjects.Add(obj);
        }
    }

    public void ClearObjects()
    {
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            if (spawnedObjects[i] != null)
                Destroy(spawnedObjects[i]);
        }

        spawnedObjects.Clear();
    }

    private void Shuffle<T>(IList<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}