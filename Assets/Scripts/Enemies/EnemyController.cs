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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyGameObject = gameObject;
        rb = gameObject.GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine();
        states = new StateList(this, stateMachine);
        stateMachine.Init(states.ForwardState);
        
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
        Destroy(gameObject);

    }
}
