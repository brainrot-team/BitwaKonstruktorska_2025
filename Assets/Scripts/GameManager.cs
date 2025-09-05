using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static UnityEvent OnGameOver = new UnityEvent();
    public static UnityEvent OnGameWon = new UnityEvent();
    public static UnityEvent<int> OnTrashInWorldChanged = new UnityEvent<int>();

    [SerializeField] float gameTime = 100f;
    public float GameTime => gameTime;

    private int trashInWorld = 0;
    public int TrashInWorld
    {
        get => trashInWorld;
        set
        {
            trashInWorld = value;
            OnTrashInWorldChanged.Invoke(trashInWorld);
            if(trashInWorld <= 0)
            {
                OnGameWon.Invoke();
            }
        }
    }

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
    
}
