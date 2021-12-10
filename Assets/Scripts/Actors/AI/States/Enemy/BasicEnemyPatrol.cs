using UnityEngine;

public class BasicEnemyPatrol : BasicEnemyPassive
{
    private int _PatrolIndex = 0;
    private int _PatrolDirection = 1;

    private Vector3 lastPosition;

    public BasicEnemyPatrol(BasicEnemyMachine stateMachine) : base("Patrol", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        switch (_PatrolParameters.patrolReturnProtocol)
        {
            case PatrolReturnProtocol.Start:
                SetPatrolIndex(0);
                break;
            case PatrolReturnProtocol.End:
                SetPatrolIndex(_PatrolParameters.patrolPoints.Length - 1);
                break;
            case PatrolReturnProtocol.Nearest:
                break;
            case PatrolReturnProtocol.Continue:
                break;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _SM.rigidbody2D.MovePosition(Vector3.MoveTowards(_SM.transform.position, _PatrolParameters.patrolPoints[_PatrolIndex], Time.fixedDeltaTime * _PatrolParameters.patrolSpeed));
        if (Vector3.Distance(_SM.transform.position, _PatrolParameters.patrolPoints[_PatrolIndex]) < 0.05)
            SetPatrolIndex(_PatrolIndex + _PatrolDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void SetPatrolIndex(int index)
    {
        if (index > _PatrolParameters.patrolPoints.Length - 1)
        {
            switch (_PatrolParameters.patrolEndProtocol)
            {
                case PatrolEndProtocol.Stop:
                    _PatrolIndex = _PatrolParameters.patrolPoints.Length;
                    stateMachine.ChangeState(_SM.stoppedState);
                    return;
                case PatrolEndProtocol.Loop:
                    _PatrolIndex = 0;
                    break;
                case PatrolEndProtocol.Mirror:
                    _PatrolIndex = _PatrolParameters.patrolPoints.Length - 2;
                    _PatrolDirection = -1;
                    break;
            }
        }
        else
        {
            _PatrolIndex = Mathf.Abs(index);
            if (index < 0)
                _PatrolDirection = 1;
        }
        lastPosition = _SM.transform.position;
    }
}
