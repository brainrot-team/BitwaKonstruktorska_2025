using UnityEngine;

public class EnemyDriveBy : EnemyShooter
{

    [SerializeField] protected Transform shootPoint1;
    [SerializeField] protected Transform shootPoint2;
    [SerializeField] protected Transform shootPoint3;
    [SerializeField] protected Transform shootPoint4;


    private RaycastHit2D hit;
    private RaycastHit2D hit2;
    private RaycastHit2D hit3;
    private RaycastHit2D hit4;

    private EnemyData enemyData;

    void Start()
    {
        base.Start();
        enemyData = gameObject.GetComponent<EnemyController>().enemyData;
    }

    void Update()
    {
        if (canShoot)
        {
            this.currentTimeAfterShoot += Time.deltaTime;
            if (currentTimeAfterShoot > delayBetween)
            {
                SpawnProjectiles();
            }
            if (shots >= bulletsInSerie)
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
        if (timeToRandomShoot > randomShotTime)
        {
            canShoot = true;
        }
    }

    void FixedUpdate()
    {
        if (currentTimeAfterSerie < reloadTime)
        {
            return;
        }

        hit = Physics2D.Raycast(shootPoint1.position, transform.up, shootDistance, hitLayers);
        hit2 = Physics2D.Raycast(shootPoint2.position, transform.up, shootDistance, hitLayers);
        hit3 = Physics2D.Raycast(shootPoint3.position, -transform.up, shootDistance, hitLayers);
        hit4 = Physics2D.Raycast(shootPoint4.position, -transform.up, shootDistance, hitLayers);

        if (canShoot) return;
        if (hit.collider != null)
        {
            if (hit.collider.gameObject == WorldManager.Instance.playerObject)
            {
                canShoot = true;
                SpawnProjectiles();
                return;
            }
        }

        if (hit2.collider != null)
        {
            if (hit2.collider.gameObject == WorldManager.Instance.playerObject)
            {
                canShoot = true;
                SpawnProjectiles();
                return;
            }
        }

        if (hit3.collider != null)
        {
            if (hit3.collider.gameObject == WorldManager.Instance.playerObject)
            {
                canShoot = true;
                SpawnProjectiles();
                return;
            }
        }

        if (hit4.collider != null)
        {
            if (hit4.collider.gameObject == WorldManager.Instance.playerObject)
            {
                canShoot = true;
                SpawnProjectiles();
                return;

            }
        }
    }

    private void SpawnProjectiles()
    {
        currentTimeAfterShoot = 0;
        WorldManager.Instance.TrashScore += 4;

        GameObject trashObject = TrashPrefabHolder.Instance.getRandomTrash(); ;
        trashObject.transform.SetPositionAndRotation(shootPoint1.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 90));
        trashObject.GetComponent<Rigidbody2D>().linearVelocity = transform.up * enemyData.bulletSpeed;
        trashObject.GetComponent<TrashProjectile>().ShootProjectile(ProjectileOrigin.Enemy);

        GameObject trashObject2 = TrashPrefabHolder.Instance.getRandomTrash(); ;
        trashObject.transform.SetPositionAndRotation(shootPoint2.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 90));
        trashObject2.GetComponent<Rigidbody2D>().linearVelocity = transform.up * enemyData.bulletSpeed;
        trashObject2.GetComponent<TrashProjectile>().ShootProjectile(ProjectileOrigin.Enemy);

        GameObject trashObject3 = TrashPrefabHolder.Instance.getRandomTrash(); ;
        trashObject.transform.SetPositionAndRotation(shootPoint3.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 90));
        trashObject3.GetComponent<Rigidbody2D>().linearVelocity = -transform.up * enemyData.bulletSpeed;
        trashObject3.GetComponent<TrashProjectile>().ShootProjectile(ProjectileOrigin.Enemy);

        GameObject trashObject4 = TrashPrefabHolder.Instance.getRandomTrash(); ;
        trashObject.transform.SetPositionAndRotation(shootPoint4.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 90));
        trashObject4.GetComponent<Rigidbody2D>().linearVelocity = -transform.up * enemyData.bulletSpeed;
        trashObject4.GetComponent<TrashProjectile>().ShootProjectile(ProjectileOrigin.Enemy);
        shots++;
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        if (shootPoint1 == null && shootPoint2 == null && shootPoint3 == null && shootPoint4 == null) return;

        Gizmos.color = Color.red;

        Gizmos.DrawLine(shootPoint1.position, shootPoint1.position + transform.up * shootDistance);
        Gizmos.DrawLine(shootPoint2.position, shootPoint2.position + transform.up * shootDistance);
        Gizmos.DrawLine(shootPoint3.position, shootPoint3.position + -transform.up * shootDistance);
        Gizmos.DrawLine(shootPoint4.position, shootPoint4.position + -transform.up * shootDistance);

    }
}
