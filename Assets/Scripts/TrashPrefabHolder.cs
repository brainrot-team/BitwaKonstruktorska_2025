
using UnityEngine;
using System.Collections.Generic;

public class TrashPrefabHolder : MonoBehaviour
{
    [SerializeField] List<AnimationClip> animations = new List<AnimationClip> ();
    [SerializeField] GameObject trashPrefab;

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
        print("BBBBB");
        var inst = Instantiate(trashPrefab);
        var index = Random.Range(0, animations.Count);
        inst.GetComponent<Animator>().Play($"animation{index+1}");
        return inst;
    }

}
