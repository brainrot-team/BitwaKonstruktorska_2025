using UnityEngine;
using UnityEngine.Events;

public class PlayerTrash : MonoBehaviour
{
    [HideInInspector] public static UnityEvent<int> OnNumberOfAttacksChanged = new UnityEvent<int>();
    
    public static PlayerTrash Instance { get; private set; }

    void Awake()
    {
        Instance = this;
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