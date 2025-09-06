using UnityEngine;

public class RotateToPlayerState : State
{
    Vector2 targetPosition;
    Transform transform;
    Rigidbody2D rb;

    private Vector3 targetPoint;

    private int multiplayer;


    public RotateToPlayerState(EnemyController enemy, StateMachine stateMachine) : base(enemy, stateMachine) { }

    public override void Enter() 
	{ 
        base.Enter();
		transform = enemy.enemyGameObject.transform;
        rb = enemy.rb;

        targetPoint = WorldManager.Instance.GetPlayerPosition();

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

        float angle = transform.rotation.eulerAngles.z + (multiplayer *  enemy.enemyData.angleSpeed + Time.deltaTime);
        transform.rotation = Quaternion.Euler(0,0,angle);
        rb.MovePosition(transform.position + (transform.right * Time.deltaTime * enemy.enemyData.speed));

        if(Vector2.Angle(transform.right,targetPoint - transform.position) < 20.0f)
        {
            if(enemy.viewRange.GetEnemyDetected())
            {
                enemy.stateMachine.Change(enemy.states.MoveTowardPlayer);
                return;
            }
            enemy.stateMachine.Change(enemy.states.ForwardState);
            return;   
        }
	}
}
