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

    public void SpawnTrashProjectile(Vector2 trashPosition, Vector2 startVelocity, ProjectileOrigin origin,Transform targetTransorm)
    {
        print("AAAAAAAAAAAAAAAAAAAAAAAAAAA");
        var projectile = TrashPrefabHolder.Instance.getRandomTrash();
            
         projectile.transform.SetPositionAndRotation( trashPosition, Quaternion.identity);

        TrashProjectile trashProjectile = projectile.GetComponent<TrashProjectile>();
        trashProjectile.ShootProjectile(origin);
        if (trashProjectile.effect != null)
        {
            trashProjectile.effect.transform.rotation = targetTransorm.rotation;
        }
        trashProjectile.GetComponent<Rigidbody2D>().linearVelocity = startVelocity;
    }    
}