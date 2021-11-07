using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code sourced from: https://medium.com/c-sharp-progarmming/make-a-basic-fsm-in-unity-c-f7d9db965134
public abstract class BaseState
{
    public string name;
    protected StateMachine stateMachine;

    public BaseState(string name, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }
}