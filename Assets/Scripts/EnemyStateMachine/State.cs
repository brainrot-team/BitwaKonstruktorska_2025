using System;
using UnityEngine;

[System.Serializable]
public class State
{
	protected StateMachine stateMachine;
	protected EnemyController enemy;

	public State(EnemyController enemy, StateMachine stateMachine)
	{
		this.enemy = enemy;
		this.stateMachine = stateMachine;
	}

	public virtual void Enter()
	{
		//Debug.Log($"Entering state: {this}");
	}

	public virtual void UpdateLogic() { }

	public virtual void UpdatePhysics() { }

	public virtual void Exit() { }
}