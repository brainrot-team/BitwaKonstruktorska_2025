using UnityEngine;
using UnityEngine.Events;

public class PlayerTrash : MonoBehaviour
{
    [HideInInspector] public static UnityEvent<int> OnNumberOfAttacksChanged = new UnityEvent<int>();
    
    public static PlayerTrash Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }
    
    
    private int collectedTrash = 100;
    public int CollectedTrash
    {
        set
        {
            if (collectedTrash <= 0) return;
            collectedTrash = value;
            OnNumberOfAttacksChanged.Invoke(collectedTrash);
        }
        get => collectedTrash;
    }
}