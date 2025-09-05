using UnityEngine;

public class PatrollState : State
{

	Vector2 targetPosition;
    Transform transform;
    Rigidbody2D rb;
    float speed = 10;


    public PatrollState(EnemyController enemy, StateMachine stateMachine) : base(enemy, stateMachine) { }

    public override void Enter() 
	{ 
        base.Enter();
		transform = enemy.enemyGameObject.transform;
        rb = enemy.rb;
        
        FindNewTargetPosition();
	}

    public override void Exit()
	{
		base.Exit();
	}

	public override void UpdateLogic() 
	{
        base.UpdateLogic();

		if(Vector2.Distance(transform.position, targetPosition) < 0.25)
        {
            enemy.stateMachine.Change(enemy.states.PatrollState);
        }

        rb.MovePosition(Vector2.MoveTowards(transform.position,targetPosition,Time.deltaTime * enemy.enemyData.speed));
        Debug.DrawLine(transform.position,rb.position);

	}

	public override void UpdatePhysics() 
	{
        base.UpdatePhysics();

	}

	private void FindNewTargetPosition()
    {
        targetPosition = (Vector2)enemy.enemyGameObject.transform.position + new Vector2(UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-5, 5));

        var bounds = WorldManager.Instance.WorldBounds;
        targetPosition.x = Mathf.Clamp(targetPosition.x, -bounds.x, bounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -bounds.y, bounds.y);
        
    }
}
