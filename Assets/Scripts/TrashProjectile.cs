
using Unity.VisualScripting;
using UnityEngine;

public enum ProjectileOrigin
{
    Player,
    Enemy,
    neutral
}

public class TrashProjectile : Trash
{
    private LayerMask targetLayer;
    
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public GameObject effect = null;

    private bool isLethal = false;

    [SerializeField] private int trashValue = 1;


    public void TurnToTrash()
    {
        gameObject.tag = "Trash";

    }

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Mathf.Abs(transform.position.x) > WorldManager.Instance.WorldBounds.x * 0.99 || Mathf.Abs(transform.position.y) > WorldManager.Instance.WorldBounds.y * 0.99)
        {
            rb.AddForce((-transform.position).normalized );
            
        }

    }

    public void SetOrigin(ProjectileOrigin origin)
    {
        switch (origin)
        {
            case ProjectileOrigin.Player:
                tag = "PlayerProjectile";
                targetLayer = LayerMask.NameToLayer("Enemy");
                rb.excludeLayers = 64;
                effect = Instantiate(TrashPrefabHolder.Instance.fireEffect);
                effect.transform.position = Vector3.zero;
                effect.transform.SetParent(transform, false);
                Invoke(nameof(EnableCollecting), 2);
                Invoke(nameof(DisableLethal), 1.5f);
                isPickUpDisabled = true;
                isLethal = true;
                spriteRenderer.color = Color.red;

                break;
            case ProjectileOrigin.Enemy:
                tag = "EnemyProjectile";
                targetLayer = LayerMask.NameToLayer("Player");
                rb.excludeLayers = 512;
                //print(rb.excludeLayers);
                Invoke(nameof(EnableCollecting), 2);
                Invoke(nameof(DisableLethal), 1.5f);
                isPickUpDisabled = true;
                isLethal = true;
                spriteRenderer.color = Color.red;
                break;
            case ProjectileOrigin.neutral:
                tag = "neutral";
                rb.excludeLayers = 512;
                Invoke(nameof(DisableCollision), 1f);
                isPickUpDisabled = false;
                

                break;
            default:
                Debug.LogError("Unknown ProjectileOrigin: " + origin);
                break;
        }
    }

    public void ShootProjectile(ProjectileOrigin origin)
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        

        
        
        SetOrigin(origin);

    }

    void EnableCollecting()
    {
        isPickUpDisabled = false;
        spriteRenderer.color = Color.gray;
    }

    void DisableCollision()
    {
        rb.excludeLayers = 0;
    }

    void DisableLethal()
    {
        rb.linearDamping = rb.linearDamping * 2;
        spriteRenderer.color = Color.yellow;

        Destroy(effect);

        isLethal = false;
        rb.excludeLayers = 0;
        //print("disabling damage");
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {




        //if (collision.CompareTag("Player"))
        //{
        //    if (isPickUpDisabled) return;
        //    Destroy(gameObject);
        //}


        if (!isPickUpDisabled)
        {

            if (collision.collider.TryGetComponent<PlayerTrash>(out PlayerTrash inputManager))
            {
                if (PlayerTrash.Instance.AddTrash(trashValue))
                {
                    Destroy(gameObject);

                }
            }
        }



        if(isLethal)
        {
            //collision.GetComponent<IHittable>();

            if(collision.collider.TryGetComponent<EnemyController>(out EnemyController enemyController))
            {
                enemyController.HitByProjectile();
                Destroy(gameObject);
                
            }

            if (collision.collider.TryGetComponent<PlayerTrash>(out PlayerTrash xxx))
            {
                if (PlayerTrash.Instance.AddTrash(trashValue))
                {
                    Destroy(gameObject);

                }
            }


            if (collision.collider.TryGetComponent<InputManager>(out InputManager inputManager))
            {
                inputManager.HitByProjectile();

                if (PlayerTrash.Instance.AddTrash(trashValue))
                {
                    Destroy(gameObject);

                }

            }

        }
    }
}