using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [HideInInspector] public GameObject enemyGameObject;
    [HideInInspector] public Rigidbody2D rb;
    //EnemyState state = new RandomPatrol();
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
        //stateMachine.Init(states.SpinningState);
        stateMachine.Init(states.ForwardState);
        //stateMachine.Init(states.ToCenterState);
        
    }

    void Update()
    {
        stateMachine.currentState.UpdateLogic();
    }

    void FixedUpdate()
    {
        stateMachine.currentState.UpdatePhysics();
    }
}
