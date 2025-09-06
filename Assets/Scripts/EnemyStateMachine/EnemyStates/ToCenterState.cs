using UnityEngine;

public class ToCenterState : State
{
    Vector2 targetPosition;
    Transform transform;
    Rigidbody2D rb;


    private Vector3 startingPosition;
    private Vector3 circleCenter;

    private Vector3 targetPoint;

    private int multiplayer;


    public ToCenterState(EnemyController enemy, StateMachine stateMachine) : base(enemy, stateMachine) { }

    public override void Enter() 
	{ 
        base.Enter();
		transform = enemy.enemyGameObject.transform;
        rb = enemy.rb;

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

        float angle = transform.rotation.eulerAngles.z + (multiplayer *  enemy.enemyData.backAngleSpeed + Time.deltaTime);
        transform.rotation = Quaternion.Euler(0,0,angle);
        rb.MovePosition(transform.position + (transform.right * Time.deltaTime * enemy.enemyData.speed));

        //if(Vector2.Angle(transform.right,targetPoint - transform.position) < 2.0f)
        //{
        //    enemy.stateMachine.Change(enemy.states.ForwardState);
        //    return;   
        //}
	}
}
