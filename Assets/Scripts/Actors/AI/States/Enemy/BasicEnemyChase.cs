using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyChase : BasicEnemyAggressive
{
    public BasicEnemyChase(BasicEnemyMachine stateMachine) : base("Chase", stateMachine)
    {
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        float distance = Vector3.Distance(_SM.transform.position, _SM.target.transform.position);
        if (distance < _SM.lungeRadius)
            stateMachine.ChangeState(_SM.lungeState);
        else if (distance > _SM.detectionRadius)
            stateMachine.ChangeState(_SM.patrolState);
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _SM.rigidbody2D.velocity = (_SM.target.transform.position - _SM.transform.position).normalized * _SM.chaseSpeed;
    }
}
