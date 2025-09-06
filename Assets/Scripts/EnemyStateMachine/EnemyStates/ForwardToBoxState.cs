using UnityEngine;

public class ForwardToBoxState : State
{
    Vector2 targetPosition;
    Transform transform;
    Rigidbody2D rb;

    private Vector3 startingPosition;

    private bool isInBox;

    private float currentTime = 0;
    private float maxTime = 0;


    public ForwardToBoxState(EnemyController enemy, StateMachine stateMachine) : base(enemy, stateMachine) { }

    public override void Enter() 
	{ 
        base.Enter();
		transform = enemy.enemyGameObject.transform;
        rb = enemy.rb;
        isInBox = WorldManager.Instance.IsInBox(transform.position);
        currentTime = 0;
        maxTime = Random.Range(2f,3f);
        
	}

    public override void Exit()
	{
		base.Exit();
	}

	public override void UpdateLogic() 
	{
        base.UpdateLogic();

        currentTime += Time.deltaTime;
        if(currentTime > maxTime)
        {   
            enemy.stateMachine.Change(enemy.states.ForwardState);
            return;
        }
	}

	public override void UpdatePhysics() 
	{
        base.UpdatePhysics();
        rb.MovePosition(transform.position + (transform.right * Time.deltaTime * enemy.enemyData.speed));
	}
}
