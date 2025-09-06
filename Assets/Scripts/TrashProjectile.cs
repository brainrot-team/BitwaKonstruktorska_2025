using System.Numerics;
using UnityEngine;

public enum ProjectileOrigin
{
    Player,
    Enemy
}

public class TrashProjectile : Trash
{
    private LayerMask targetLayer;
    
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private bool isLethal = false;

    [SerializeField] private int trashValue = 1;


    public void TurnToTrash()
    {
        gameObject.tag = "Trash";

    }

    public void SetOrigin(ProjectileOrigin origin)
    {
        switch (origin)
        {
            case ProjectileOrigin.Player:
                tag = "PlayerProjectile";
                targetLayer = LayerMask.NameToLayer("Enemy");
                rb.excludeLayers = LayerMask.NameToLayer("Player");
                break;
            case ProjectileOrigin.Enemy:
                tag = "EnemyProjectile";
                targetLayer = LayerMask.NameToLayer("Player");
                rb.excludeLayers = LayerMask.NameToLayer("Enemy");
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

        spriteRenderer.color = Color.red;

        isPickUpDisabled = true;
        isLethal = true;
        Invoke(nameof(EnableCollecting), 2);
        Invoke(nameof(DisableLethal), 1.5f);
        SetOrigin(origin);

    }

    void EnableCollecting()
    {
        isPickUpDisabled = false;
        spriteRenderer.color = Color.gray;
    }

    void DisableLethal()
    {
        rb.linearDamping = rb.linearDamping * 2;
        spriteRenderer.color = Color.yellow;

        isLethal = false;
        rb.excludeLayers = 0;
        print("disabling damage");
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
                
            }

            if (collision.collider.TryGetComponent<InputManager>(out InputManager inputManager))
            {
                inputManager.HitByProjectile();

            }

        }
    }
}