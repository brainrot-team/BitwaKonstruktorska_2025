
using UnityEngine;
using System.Collections.Generic;

public class TrashPrefabHolder : MonoBehaviour
{
    [SerializeField] List<GameObject> trashPrefabs = new List<GameObject> ();

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

        int index = Random.Range (0, trashPrefabs.Count);
        return trashPrefabs [index];
    }

}
