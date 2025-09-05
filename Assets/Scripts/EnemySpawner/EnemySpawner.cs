using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefab;
    [SerializeField] private float spawnDelay = 5.0f;
    [SerializeField] private int enemyMaxNumber = 3;

    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();
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
        
        int randIndex = Random.Range(0,enemyPrefab.Count);

        spawnedEnemies.RemoveAll(enemy => enemy == null);

        if(enemyMaxNumber > spawnedEnemies.Count)
        {
            GameObject enemy =  Instantiate(enemyPrefab[randIndex], transform.position, Quaternion.identity);
            spawnedEnemies.Add(enemy);
        }
    }
}
