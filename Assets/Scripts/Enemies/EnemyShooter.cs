using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("Basics")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] protected GameObject trashBullet;

    [SerializeField] protected LayerMask hitLayers;
    [SerializeField] protected float shootDistance;


    [Header("Serie Shot")]
    [SerializeField] protected float delayBetween = 0.5f;
    [SerializeField] protected int bulletsInSerie = 3;


    [Header("Delay Next Serie")]
    [SerializeField] protected float reloadTime = 1.0f;

    [Header("Random Shot")]
    [SerializeField] protected float minDelay = 10.0f;
    [SerializeField] protected float maxDelay = 30.0f;

    protected int shots = 0;
    protected float currentTimeAfterShoot = 0;
    protected float currentTimeAfterSerie = 0;

    protected float timeToRandomShoot = 0;
    protected float randomShotTime = 0;

    protected bool canShoot = false;

    private RaycastHit2D hit;

    protected void Start()
    {
        randomShotTime = Random.Range(minDelay,maxDelay);
    }

    void Update()
    {
        if(canShoot)
        {
            currentTimeAfterShoot += Time.deltaTime;
            if(currentTimeAfterShoot > delayBetween)
            {
                SpawnProjectile();
            }
            if(shots >= bulletsInSerie)
            {
                shots = 0;
                canShoot = false;
                currentTimeAfterSerie = 0;
                timeToRandomShoot = 0;
            }
            return;
        }
        else
        {
            currentTimeAfterSerie += Time.deltaTime;
            timeToRandomShoot += Time.deltaTime;
        }
        if(timeToRandomShoot > randomShotTime)
        {
            canShoot = true;
        }
    }

    void FixedUpdate()
    {
        if(currentTimeAfterSerie < reloadTime)
        {
            return;
        }

        hit = Physics2D.Raycast(shootPoint.position, transform.right,shootDistance,hitLayers);

        if(hit.collider != null)
        {
            if(hit.collider.gameObject == WorldManager.Instance.playerObject && !canShoot)
            {
                canShoot = true;
                SpawnProjectile();
            }
        }
    }

    private void SpawnProjectile()
    {
        SoundManager.Instance?.PlaySound(SoundEffectType.EnemyShoot);
        currentTimeAfterShoot = 0;
        GameObject trashObject = TrashPrefabHolder.Instance.getRandomTrash();
        trashObject.transform.SetPositionAndRotation(shootPoint.position, transform.rotation);
        WorldManager.Instance.TrashScore++;
        
        trashObject.GetComponent<Rigidbody2D>().linearVelocity = transform.right * 5;
        trashObject.GetComponent<TrashProjectile>().ShootProjectile(ProjectileOrigin.Enemy);
        shots++;
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        if (shootPoint == null) return;
        
        Gizmos.color = hit.collider != null ? Color.red : Color.green;
        
        Gizmos.DrawLine(shootPoint.position, shootPoint.position + (Vector3)transform.right * shootDistance);
        
    }
    
}
