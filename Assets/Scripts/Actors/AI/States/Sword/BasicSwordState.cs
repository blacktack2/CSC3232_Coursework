
public abstract class BasicSwordState : BaseState
{
    protected BasicSwordMachine _SM;
    protected BasicSwordMachine.StateParameters _StateParameters;
    protected BasicSwordMachine.StateParameters.Collectable _CollectableParameters;
    protected BasicSwordMachine.StateParameters.Idle _IdleParameters;
    protected BasicSwordMachine.StateParameters.Idle.Follow _FollowParameters;
    protected BasicSwordMachine.StateParameters.Idle.Hover _HoverParameters;
    protected BasicSwordMachine.StateParameters.Attack _AttackParameters;
    protected BasicSwordMachine.StateParameters.Attack.Primary _PrimaryParameters;
    protected BasicSwordMachine.StateParameters.Attack.Secondary _SecondaryParameters;

    public BasicSwordState(string name, BasicSwordMachine stateMachine) : base(name, stateMachine)
    {
        _SM = stateMachine;
        _StateParameters = _SM.stateParameters;
        _CollectableParameters = _StateParameters.collectable;
        _IdleParameters = _StateParameters.idle;
        _FollowParameters = _IdleParameters.follow;
        _HoverParameters = _IdleParameters.hover;
        _AttackParameters = _StateParameters.attack;
        _PrimaryParameters = _AttackParameters.primary;
        _SecondaryParameters = _AttackParameters.secondary;
    }
}
