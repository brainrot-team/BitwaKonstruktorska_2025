using UnityEngine;
using System.Collections.Generic;

public class TrashSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> TrashPrefab;
    [SerializeField] private float spawnDelay = 5.0f;

    private float currentTime = 0;

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > spawnDelay)
        {
            currentTime = 0.0f;
            Spawn();    
        }
    }

    public void Spawn()
    {
        int randIndex = Random.Range(0,TrashPrefab.Count);
        GameObject enemy =  Instantiate(TrashPrefab[randIndex], transform.position, Quaternion.identity);
    }
}
