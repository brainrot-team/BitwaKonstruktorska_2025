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
    
    
    [SerializeField] private int collectedTrash = 5;
    [SerializeField] private int maxTrashCapacity = 200;

    public int CollectedTrash
    {
        set
        {
            
            collectedTrash = value;
            collectedTrash = Mathf.Clamp(collectedTrash, 0, int.MaxValue);
            OnNumberOfAttacksChanged.Invoke(collectedTrash);

        }
        get => collectedTrash;
    }

    public bool AddTrash(int trashToAdd = 1)
    {
        if(CollectedTrash + trashToAdd <= maxTrashCapacity)
        {
            CollectedTrash += trashToAdd;
            SoundManager.Instance.PlaySound3(SoundEffectType.PickUp);
            return true;
        }
        print("too much trash");
        return false;

    }
}