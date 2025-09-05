using UnityEngine;

public class SpinningState : State
{
    Vector2 targetPosition;
    Transform transform;
    Rigidbody2D rb;
    float speed = 10;


    private Vector3 startingPosition;
    private Vector3 circleCenter;
    private float circleAngle;


    public SpinningState(EnemyController enemy, StateMachine stateMachine) : base(enemy, stateMachine) { }

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
        startingPosition = transform.position;
        Vector3 currentPosition = CalculateMovement();
        rb.MovePosition(currentPosition);

        Vector3 direction = currentPosition - startingPosition;
        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,angle);

	}

    private Vector3 CalculateMovement()
    {
        circleAngle += enemy.enemyData.angleSpeed * Time.deltaTime;
        float xOffset = Mathf.Cos(circleAngle) * enemy.enemyData.circlingRadius;
        float yOffset = Mathf.Sin(circleAngle) * enemy.enemyData.circlingRadius;

        return new Vector3(startingPosition.x + xOffset, startingPosition.y + yOffset,0);
    }
}
