
using UnityEngine;
using System.Collections.Generic;

public class TrashPrefabHolder : MonoBehaviour
{
    [SerializeField] private int numberOfDifferentEnemies = 17;
    [SerializeField] GameObject trashPrefab;
    public GameObject fireEffect;

    public static TrashPrefabHolder Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject getRandomTrash()
    {

        var inst = Instantiate(trashPrefab);
        var index = Random.Range(0, numberOfDifferentEnemies);
        inst.GetComponent<Animator>().Play($"animation{index + 1}");
        return inst;
    }

}
