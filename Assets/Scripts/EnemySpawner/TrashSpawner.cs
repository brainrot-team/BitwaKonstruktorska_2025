using UnityEngine;
using System.Collections.Generic;

public class TrashSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> TrashPrefab;
    [SerializeField] private float spawnDelay = 5.0f;
    [SerializeField] private GameObject trashBin;

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
        var prefab = TrashPrefabHolder.Instance.getRandomTrash();
        GameObject enemy =  Instantiate(TrashPrefabHolder.Instance.getRandomTrash(), transform.position, Quaternion.identity);
        if(trashBin != null)
        {
            enemy.transform.SetParent(trashBin.transform);
        }
        WorldManager.Instance.IncreamentTrashScore();
    }
}
