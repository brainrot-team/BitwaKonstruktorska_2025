using UnityEngine;

public class ForwardState : State
{
    Vector2 targetPosition;
    Transform transform;
    Rigidbody2D rb;
    float speed = 10;


    private Vector3 startingPosition;

    private bool isInBox;

    private float currentTime = 0;
    private float maxTime = 0;


    public ForwardState(EnemyController enemy, StateMachine stateMachine) : base(enemy, stateMachine) { }

    public override void Enter() 
	{ 
        base.Enter();
		transform = enemy.enemyGameObject.transform;
        rb = enemy.rb;
        isInBox = WorldManager.Instance.IsInBox(transform.position);
        currentTime = 0;
        maxTime = Random.Range(enemy.enemyData.minStateDuration,enemy.enemyData.maxStateDuaration);
        
	}

    public override void Exit()
	{
		base.Exit();
	}

	public override void UpdateLogic() 
	{
        base.UpdateLogic();
        if(enemy.viewRange.GetEnemyDetected())
        {
            enemy.stateMachine.Change(enemy.states.RotateToPlayerState);
            return;
        }

        currentTime += Time.deltaTime;
        if(currentTime > maxTime)
        {
            if(!WorldManager.Instance.IsInBox(transform.position))
            {
                enemy.stateMachine.Change(enemy.states.ToCenterState);
                return;
            }
            enemy.stateMachine.Change(enemy.states.SpinningState);
            return;
        }
	}

	public override void UpdatePhysics() 
	{
        base.UpdatePhysics();
        rb.MovePosition(transform.position + (transform.right * Time.deltaTime * enemy.enemyData.speed));
        if(WorldManager.Instance.IsInBox(transform.position))
        {
            isInBox = true;
        }
        if(!isInBox)
        {
            return;
        }
        if(!WorldManager.Instance.IsInBox(transform.position))
        {
            enemy.stateMachine.Change(enemy.states.ToCenterState);
            return;
        }

	}
}
