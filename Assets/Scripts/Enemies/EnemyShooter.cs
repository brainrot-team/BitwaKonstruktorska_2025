using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("Basics")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject trashBullet;

    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private float shootDistance;


    [Header("Serie Shot")]
    [SerializeField] private float delayBetween = 0.5f;
    [SerializeField] private int bulletsInSerie = 3;


    [Header("Delay Next Serie")]
    [SerializeField] private float reloadTime = 1.0f;

    [Header("Random Shot")]
    [SerializeField] private float minDelay = 10.0f;
    [SerializeField] private float maxDelay = 30.0f;

    private int shots = 0;
    private float currentTimeAfterShoot = 0;
    private float currentTimeAfterSerie = 0;

    private float timeToRandomShoot = 0;
    private float randomShotTime = 0;

    private bool canShoot = false;

    private RaycastHit2D hit;

    void Start()
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
        currentTimeAfterShoot = 0;
        GameObject trashObject = Instantiate(trashBullet, shootPoint.position, transform.rotation);
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
