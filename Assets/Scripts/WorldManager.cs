using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance;
    
    
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
}
