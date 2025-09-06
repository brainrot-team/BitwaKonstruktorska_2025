using UnityEngine;

public class SpinningState : State
{
    Vector2 targetPosition;
    Transform transform;
    Rigidbody2D rb;
    float speed = 10;


    private Vector3 startingPosition;

    private int multiplayer;
    private float currentTime = 0;
    private float maxTime = 0;


    public SpinningState(EnemyController enemy, StateMachine stateMachine) : base(enemy, stateMachine) { }

    public override void Enter() 
	{ 
        base.Enter();
		transform = enemy.enemyGameObject.transform;
        rb = enemy.rb;
        
        multiplayer = Random.Range(1,100) < 50 ? -1 : 1;
        multiplayer = enemy.lastMultiplayer;
        
        maxTime = Random.Range(enemy.enemyData.minStateDuration,enemy.enemyData.maxStateDuaration);
        currentTime = 0;
        
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
            if(enemy.viewRange.GetEnemyDetected())
            {
                enemy.stateMachine.Change(enemy.states.RotateToPlayerState);
                return;
            }
            if(!WorldManager.Instance.IsInBox(transform.position))
            {
                enemy.stateMachine.Change(enemy.states.ToCenterState);
                return;
            }
            enemy.stateMachine.Change(enemy.states.ForwardState);
            return;
        }
	}

	public override void UpdatePhysics() 
	{
        base.UpdatePhysics();
        float angle = transform.rotation.eulerAngles.z + (multiplayer * enemy.enemyData.angleSpeed + Time.deltaTime);
        transform.rotation = Quaternion.Euler(0,0,angle);
        rb.MovePosition(transform.position + (transform.right * Time.deltaTime * enemy.enemyData.speed));

	}
}
