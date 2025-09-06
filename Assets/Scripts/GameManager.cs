using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static UnityEvent<string> OnGameOver = new UnityEvent<string>();
    public static UnityEvent OnGameWon = new UnityEvent();

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

        OnGameOver.AddListener((_) => Time.timeScale = 0f);
        OnGameWon.AddListener(() => Time.timeScale = 0f);
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;
    }
}
