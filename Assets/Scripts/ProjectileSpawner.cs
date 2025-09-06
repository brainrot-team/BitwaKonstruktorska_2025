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

    public void SpawnTrashProjectile(Vector2 trashPosition, Vector2 startVelocity, ProjectileOrigin origin)
    {
        var projectile = Instantiate(projectilePrefab, trashPosition, Quaternion.identity);
        TrashProjectile trashProjectile = projectile.GetComponent<TrashProjectile>();
        trashProjectile.ShootProjectile(origin);
        trashProjectile.GetComponent<Rigidbody2D>().linearVelocity = startVelocity;
    }    
}