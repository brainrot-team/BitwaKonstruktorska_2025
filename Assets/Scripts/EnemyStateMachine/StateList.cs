using System;

[System.Serializable]
public class StateList
{
    public enum StateEnum{
        NoneState = 0,
        PatrollState = 1,
        SpinningState = 2,
        ForwardState = 3,
        ToCenterState = 4,
        RotateToPlayerState = 5,
        MoveTowardPlayer = 6,
        EscapeState = 7

    }
    
    public NoneState NoneState => (NoneState)states[0];
    public PatrollState PatrollState => (PatrollState)states[1];
    public SpinningState SpinningState => (SpinningState)states[2];
    public ForwardState ForwardState => (ForwardState)states[3];
    public ToCenterState ToCenterState => (ToCenterState)states[4];
    public RotateToPlayerState RotateToPlayerState => (RotateToPlayerState)states[5];
    public MoveTowardPlayer MoveTowardPlayer => (MoveTowardPlayer)states[6];
    public EscapeState EscapeState => (EscapeState)states[7];

    public State GetState(StateEnum stateEnum) => states[Convert.ToInt32(stateEnum)];

    public StateEnum GetStateEnum(State state) => (StateEnum)Array.IndexOf(states,state);

    private State[] states;

    public StateList(EnemyController enemy, StateMachine stateMachine)
    {
        states = new State[8];
        states[0] = new NoneState(enemy, stateMachine); 
        states[1] = new PatrollState(enemy, stateMachine); 
        states[2] = new SpinningState(enemy, stateMachine);
        states[3] = new ForwardState(enemy, stateMachine);
        states[4] = new ToCenterState(enemy, stateMachine);
        states[5] = new RotateToPlayerState(enemy, stateMachine);
        states[6] = new MoveTowardPlayer(enemy, stateMachine);
        states[7] = new EscapeState(enemy, stateMachine);
    }

}