using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    public void SpawnObjects(GameObject correct, GameObject[] wrong)
    {
        ClearObjects();

        if (spawnPoints.Length < 3)
        {
            Debug.LogError("Need at least 3 spawn points.");
            return;
        }

        if (wrong == null || wrong.Length < 2)
        {
            Debug.LogError("Each letter needs at least 2 wrong objects.");
            return;
        }

        GameObject[] objects = new GameObject[3];
        objects[0] = correct;
        objects[1] = wrong[0];
        objects[2] = wrong[1];

        Shuffle(objects);

        for (int i = 0; i < 3; i++)
        {
            GameObject obj = Instantiate(objects[i], spawnPoints[i].position, spawnPoints[i].rotation);
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

    void Shuffle(GameObject[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            GameObject temp = array[i];
            int randomIndex = Random.Range(i, array.Length);
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}