using UnityEngine;

public class ProjectileSpawner : MonoBehaviour 
{
    [Header("Spawning Settings")]
    [SerializeField] GameObject projectilePrefab;

    public static ProjectileSpawner Instance { get; private set; }

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

    public void SpawnTrashProjectile(Vector2 trashPosition, Vector2 startVelocity)
    {
        GameObject trashProjectile = Instantiate(projectilePrefab, trashPosition, Quaternion.identity);
        trashProjectile.GetComponent<Rigidbody2D>().linearVelocity = startVelocity;
    }    
}