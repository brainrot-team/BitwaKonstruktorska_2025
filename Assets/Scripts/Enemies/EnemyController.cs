using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [HideInInspector] public GameObject enemyGameObject;
    [HideInInspector] public Rigidbody2D rb;

    public EnemyViewRange viewRange;
    public StateMachine stateMachine;

    public StateList states;
    public EnemyData enemyData;

    public int lastMultiplayer = 0;

    public int currentHealth;

    // Sprite flashing
    public SpriteRenderer spriteRenderer { get; private set; }
    private Material material;
    private IEnumerator flashCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyGameObject = gameObject;
        rb = gameObject.GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine();
        states = new StateList(this, stateMachine);
        stateMachine.Init(states.ForwardState);
        //stateMachine.Init(states.ToCenterState);
        currentHealth = enemyData.maxHP;

        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }

    void Update()
    {
        stateMachine.currentState.UpdateLogic();
    }

    void FixedUpdate()
    {
        stateMachine.currentState.UpdatePhysics();



    }

    public void HitByProjectile()
    {
        print("YOU are hit");
        currentHealth--;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            SpawnProjectileOnDeath();
        }

    }

    private void SpawnProjectileOnDeath()
    {

        print("YOU ARE DEAD");

        WorldManager.Instance.TrashScore++;
        for(int i =0;i<enemyData.maxHP +1;i++)
        {
            
            GameObject trashObject = TrashPrefabHolder.Instance.getRandomTrash();

            var pom = ((float)i / (enemyData.maxHP +1)) * 2 * Mathf.PI;
            trashObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(Mathf.Sin(pom),Mathf.Cos(pom));
            trashObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
            trashObject.GetComponent<TrashProjectile>().ShootProjectile(ProjectileOrigin.neutral);
        }

    }
    
    
    public void FlashOnDamage()
    {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = FlashCoroutine();
        StartCoroutine(flashCoroutine);
    }

    private IEnumerator FlashCoroutine()
    {
        float lerpTime = 0;

        while (lerpTime < 0.3f)
        {
            lerpTime += Time.deltaTime;
            float perc = lerpTime / 0.5f;

            SetFlashAmount(1f - perc);
            yield return null;
        }
        SetFlashAmount(0);
    }

    private void SetFlashAmount(float flashAmount)
    {
        material.SetFloat("_FlashAmount", flashAmount);
    }
}
