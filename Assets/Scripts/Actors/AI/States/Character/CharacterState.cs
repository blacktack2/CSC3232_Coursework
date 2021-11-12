
public abstract class CharacterState : BaseState
{
    protected CharacterMachine _SM;
    protected CharacterMachine.StateParameters _StateParameters;
    protected CharacterMachine.StateParameters.Move _MoveParameters;

    protected CharacterState(string name, CharacterMachine stateMachine) : base(name, stateMachine)
    {
        _SM = stateMachine;
        _StateParameters = _SM.stateParameters;
        _MoveParameters = _StateParameters.move;
    }
}
