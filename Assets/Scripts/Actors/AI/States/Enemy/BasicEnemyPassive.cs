using UnityEngine;

using Pathfinding;

public abstract class BasicEnemyPassive : BasicEnemyState
{
    public BasicEnemyPassive(string name, BasicEnemyMachine stateMachine) : base(name, stateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        _SM.animator.SetTrigger("doPassive");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (CanSeeTarget())
            stateMachine.ChangeState(_SM.chaseState);
    }
}
