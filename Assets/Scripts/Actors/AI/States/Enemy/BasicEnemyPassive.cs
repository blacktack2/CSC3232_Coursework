using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicEnemyPassive : BasicEnemyState
{
    public BasicEnemyPassive(string name, BasicEnemyMachine stateMachine) : base(name, stateMachine)
    {
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (Vector3.Distance(_SM.transform.position, _SM.target.transform.position) <= _SM.detectionRadius)
            stateMachine.ChangeState(_SM.chaseState);
    }
}
