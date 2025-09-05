using UnityEngine;

public class ForwardState : State
{
    Vector2 targetPosition;
    Transform transform;
    Rigidbody2D rb;
    float speed = 10;


    private Vector3 startingPosition;
    private Vector3 circleCenter;
    private float circleAngle;


    public ForwardState(EnemyController enemy, StateMachine stateMachine) : base(enemy, stateMachine) { }

    public override void Enter() 
	{ 
        base.Enter();
		transform = enemy.enemyGameObject.transform;
        rb = enemy.rb;
        circleCenter = new Vector3(transform.position.x - enemy.enemyData.circlingRadius,transform.position.y,0);
        
	}

    public override void Exit()
	{
		base.Exit();
	}

	public override void UpdateLogic() 
	{
        base.UpdateLogic();
        

	}

	public override void UpdatePhysics() 
	{
        base.UpdatePhysics();
        rb.MovePosition(transform.position + (transform.right * Time.deltaTime * enemy.enemyData.speed));

	}
}
