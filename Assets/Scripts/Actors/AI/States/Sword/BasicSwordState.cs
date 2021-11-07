using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicSwordState : BaseState
{
    protected BasicSwordMachine _SM;

    public BasicSwordState(string name, BasicSwordMachine stateMachine) : base(name, stateMachine)
    {
        _SM = stateMachine;
    }
}