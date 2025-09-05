using UnityEngine;

public class NoneState : State
{
    public NoneState(EnemyController enemy, StateMachine stateMachine) : base(enemy, stateMachine) { }

    public override void Enter() 
	{ 
        base.Enter();
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

	}
}
