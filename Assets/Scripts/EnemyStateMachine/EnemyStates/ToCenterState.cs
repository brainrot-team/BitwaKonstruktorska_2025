using UnityEngine;

public class ToCenterState : State
{
    Vector2 targetPosition;
    Transform transform;
    Rigidbody2D rb;


    private Vector3 startingPosition;
    private Vector3 circleCenter;
    private float circleAngle;

    private Vector3 targetPoint;

    private int multiplayer;


    public ToCenterState(EnemyController enemy, StateMachine stateMachine) : base(enemy, stateMachine) { }

    public override void Enter() 
	{ 
        base.Enter();
		transform = enemy.enemyGameObject.transform;
        rb = enemy.rb;
        circleCenter = new Vector3(transform.position.x - enemy.enemyData.backCirclingRadius,transform.position.y,0);

        targetPoint = WorldManager.Instance.GetRandomPointInBox();

        if(enemy.lastMultiplayer == 0)
        {
            multiplayer = Random.Range(1,100) < 50 ? -1 : 1;
        }
        else
        {
            multiplayer = enemy.lastMultiplayer;
        }
        
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

        if(Vector2.Angle(transform.right,targetPoint - transform.position) < 2.0f)
        {
            enemy.stateMachine.Change(enemy.states.ForwardState);
            return;   
        }
	}

    private Vector3 CalculateMovement()
    {
        circleAngle += enemy.enemyData.backAngleSpeed * Time.deltaTime;
        float xOffset = Mathf.Cos(circleAngle) * enemy.enemyData.circlingRadius;
        float yOffset = Mathf.Sin(circleAngle) * enemy.enemyData.circlingRadius;

        return new Vector3(startingPosition.x + xOffset, startingPosition.y + yOffset,0);
    }
}
