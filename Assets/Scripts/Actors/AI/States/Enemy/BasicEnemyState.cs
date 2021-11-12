
public class BasicEnemyState : BaseState
{
    protected BasicEnemyMachine _SM;
    protected BasicEnemyMachine.StateParameters _StateParameters;
    protected BasicEnemyMachine.StateParameters.Passive _PassiveParameters;
    protected BasicEnemyMachine.StateParameters.Passive.Patrol _PatrolParameters;
    protected BasicEnemyMachine.StateParameters.Passive.Stopped _StoppedParameters;
    protected BasicEnemyMachine.StateParameters.Aggressive _AggressiveParameters;
    protected BasicEnemyMachine.StateParameters.Aggressive.Chase _ChaseParameters;
    protected BasicEnemyMachine.StateParameters.Aggressive.Lunge _LungeParameters;

    public BasicEnemyState(string name, BasicEnemyMachine stateMachine) : base(name, stateMachine)
    {
        _SM = stateMachine;
        _StateParameters = _SM.stateParameters;
        _PassiveParameters = _StateParameters.passive;
        _PatrolParameters = _PassiveParameters.patrol;
        _StoppedParameters = _PassiveParameters.stopped;
        _AggressiveParameters = _StateParameters.aggressive;
        _ChaseParameters = _AggressiveParameters.chase;
        _LungeParameters = _AggressiveParameters.lunge;
    }
}
