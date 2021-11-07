using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState : BaseState
{
    protected CharacterMachine _SM;

    protected CharacterState(string name, CharacterMachine stateMachine) : base(name, stateMachine)
    {
        _SM = stateMachine;
    }
}
