using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance { get; private set; }
    public static UnityEvent<int> OnTrashScoreChanged = new UnityEvent<int>();

    public GameObject playerObject;
    
    private int trashScore = 0;
    public int TrashScore {
        get => trashScore;
        set
        {
            trashScore = value;
            OnTrashScoreChanged.Invoke(trashScore);
            if (trashScore <= 0)
            {
                GameManager.OnGameWon.Invoke();
            }
        }
    }



    [SerializeField] private int startingTrash = 50;
    
    [SerializeField] public Vector2 WorldBounds = new Vector2(4,4);

    [SerializeField] private List<GameObject> allTrash= new List<GameObject>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < startingTrash; i++)
        {
            
            float x = UnityEngine.Random.Range(-WorldBounds.x, WorldBounds.x);
            float y = UnityEngine.Random.Range(-WorldBounds.x, WorldBounds.x);

            int index = Random.Range(0,allTrash.Count);


            var trash = TrashPrefabHolder.Instance.getRandomTrash();
            IncreamentTrashScore();


            trash.transform.position = new Vector3(x,y,0);
            trash.transform.rotation = Quaternion.Euler(0,0,Random.Range(0,360));


        }

        trashScore += playerObject.GetComponent<PlayerTrash>().CollectedTrash;


    }


    // Update is called once per frame
    void Update()
    {
        
        float xbound = WorldBounds.x,ybound = WorldBounds.y;

        Debug.DrawLine(new Vector2(xbound, ybound), new Vector2(-xbound, ybound), Color.red);
        Debug.DrawLine(new Vector2(xbound, ybound), new Vector2(xbound, -ybound), Color.red);

        Debug.DrawLine(new Vector2(xbound, -ybound), new Vector2(-xbound, -ybound), Color.red);
        Debug.DrawLine(new Vector2(-xbound, ybound), new Vector2(-xbound, -ybound), Color.red);

    }

    public void IncreamentTrashScore()
    {
        TrashScore++;
    }

    public bool IsInBox(Vector3 point)
    {
        return WorldBounds.x > point.x && -WorldBounds.x < point.x &&
                WorldBounds.y > point.y && -WorldBounds.y < point.y;
    }

    public Vector3 GetRandomPointInBox()
    {
        return new Vector3(Random.Range(-WorldBounds.x,WorldBounds.x),Random.Range(-WorldBounds.y,WorldBounds.y),0); 
    }

    public Vector3 GetPlayerPosition()
    {
        return playerObject.transform.position;
    }

}
