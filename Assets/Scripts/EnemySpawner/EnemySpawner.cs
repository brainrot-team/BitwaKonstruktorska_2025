using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int spawnOnStart = 0;
    [SerializeField] private List<GameObject> enemyPrefab;
    [SerializeField] private float spawnDelay = 5.0f;
    [SerializeField] private int enemyMaxNumber = 3;

    [SerializeField] private float xNoSpawnZone = 3;
    [SerializeField] private float yNoSpawnZone = 3;

    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();
    private float currentTime = 0;

    void Start()
    {
        for(int i=0;i<spawnOnStart;i++)
        {
            Spawn();
        }
    }

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

        Vector3 spawnPosition = new Vector3(Random.Range(-GetWorldBounds().x,GetWorldBounds().x), Random.Range(-GetWorldBounds().y,GetWorldBounds().y),0);

        spawnedEnemies.RemoveAll(enemy => enemy == null);

        if(enemyMaxNumber > spawnedEnemies.Count)
        {
            GameObject enemy =  Instantiate(enemyPrefab[randIndex], spawnPosition, Quaternion.identity);
            spawnedEnemies.Add(enemy);
        }
    }

    private Vector2 GetWorldBounds()
    {
        return WorldManager.Instance.WorldBounds;
    }
}
