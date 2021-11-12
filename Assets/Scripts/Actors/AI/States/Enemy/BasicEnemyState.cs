
using UnityEngine;

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

    private Vector3 _LastPos;
    private Vector3 _InitialScale;
    private Vector3 _FlippedScale;
    private bool _FacingRight = false;

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

        _LastPos = _SM.transform.position;
        _InitialScale = _SM.transform.localScale;
        _FlippedScale = Vector3.Scale(_InitialScale, new Vector3(-1, 1, 1));
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        Vector3 currentPos = _SM.transform.position;
        if (_FacingRight && currentPos.x < _LastPos.x)
        {
            _SM.transform.localScale = _InitialScale;
            _FacingRight = false;
        }
        else if (!_FacingRight && currentPos.x > _LastPos.x)
        {
            _SM.transform.localScale = _FlippedScale;
            _FacingRight = true;
        }
        _LastPos = currentPos;
    }
}
