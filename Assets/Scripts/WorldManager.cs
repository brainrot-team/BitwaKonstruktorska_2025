using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance;

    public GameObject playerObject;
    
    public int trashScore = 0;
    
    [SerializeField] public Vector2 WorldBounds = new Vector2(4,4);

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
        trashScore++;
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
