using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicEnemyAggressive : BasicEnemyState
{
    protected BasicEnemyAggressive(string name, BasicEnemyMachine stateMachine) : base(name, stateMachine)
    {
    }
}
