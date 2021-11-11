using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyPatrol : BasicEnemyPassive
{
    private int _PatrolIndex = 0;
    private int _PatrolDirection = 1;

    private float _MoveTime = 0.0f;
    private Vector3 lastPosition;
    private float currentVelocity = 1.0f;

    public BasicEnemyPatrol(BasicEnemyMachine stateMachine) : base("Patrol", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        switch (_SM.patrolReturnProtocol)
        {
            case PatrolReturnProtocol.Start:
                SetPatrolIndex(0);
                break;
            case PatrolReturnProtocol.End:
                SetPatrolIndex(_SM.patrolPoints.Length - 1);
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
        _SM.transform.position = Vector3.MoveTowards(_SM.transform.position, _SM.patrolPoints[_PatrolIndex], Time.fixedDeltaTime * _SM.patrolSpeed);
        if (Vector3.Distance(_SM.transform.position, _SM.patrolPoints[_PatrolIndex]) < 0.05)
            SetPatrolIndex(_PatrolIndex + _PatrolDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void SetPatrolIndex(int index)
    {
        if (index > _SM.patrolPoints.Length - 1)
        {
            switch (_SM.patrolEndProtocol)
            {
                case PatrolEndProtocol.Stop:
                    _PatrolIndex = _SM.patrolPoints.Length;
                    stateMachine.ChangeState(_SM.stoppedState);
                    return;
                case PatrolEndProtocol.Loop:
                    _PatrolIndex = 0;
                    break;
                case PatrolEndProtocol.Mirror:
                    _PatrolIndex = _SM.patrolPoints.Length - 2;
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
        _MoveTime = 0.0f;
        lastPosition = _SM.transform.position;
    }
}
