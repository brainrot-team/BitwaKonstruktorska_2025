using UnityEngine;

public class MoveTowardPlayer : State
{
    Vector2 targetPosition;
    Transform transform;
    Rigidbody2D rb;

    public MoveTowardPlayer(EnemyController enemy, StateMachine stateMachine) : base(enemy, stateMachine) { }

    public override void Enter() 
	{ 
        base.Enter();
		transform = enemy.enemyGameObject.transform;
        rb = enemy.rb;
        
	}

    public override void Exit()
	{
		base.Exit();
	}

	public override void UpdateLogic() 
	{
        base.UpdateLogic();
        
        if(!enemy.viewRange.GetEnemyDetected())
        {
            enemy.stateMachine.Change(enemy.states.SpinningState);
        }
	}

	public override void UpdatePhysics() 
	{
        base.UpdatePhysics();
        Vector3 targetPoint = WorldManager.Instance.GetPlayerPosition();

        float angle = Vector2.Angle(transform.right,targetPoint - transform.position);
        
        float multiplayer = 0.0f;
        if(angle > 2.0f)
        {
            multiplayer = -1.0f;
        }
        else if(angle < -2.0f)
        {
            multiplayer = 1.0f;
        }

        rb.MovePosition(transform.position + (transform.right * Time.deltaTime * enemy.enemyData.attackSpeed));
        

	}
}
