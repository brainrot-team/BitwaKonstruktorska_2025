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
        currentHealth--;
        if (currentHealth <= 0)
            Destroy(gameObject);

    }

    private void SpawnProjectileOnDeath()
    {


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
