using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyState : BaseState
{
    protected BasicEnemyMachine _SM;

    public BasicEnemyState(string name, BasicEnemyMachine stateMachine) : base(name, stateMachine)
    {
        _SM = stateMachine;
    }
}
