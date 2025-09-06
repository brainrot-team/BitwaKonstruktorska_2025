using UnityEngine;

public class StateMachine
{
    public State currentState;

    public void Init(State state)
    {
        currentState = state;
        ShowState();
        currentState.Enter();
    }

    public void Change(State newState) 
    {
        currentState.Exit();
        currentState = newState;
        Init(currentState);
    }

    public void ShowState()
    {
       //Debug.Log(currentState);
    }
}