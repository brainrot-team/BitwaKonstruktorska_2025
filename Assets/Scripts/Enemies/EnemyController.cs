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
}
