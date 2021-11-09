using UnityEngine;

public class BasicSwordCollectable : BasicSwordState
{
    public BasicSwordCollectable(BasicSwordMachine stateMachine) : base("Collectable", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _SM.collectableTrigger.enabled = true;
        _SM.animator.SetBool("isCollectable", true);
    }

    public override void Exit()
    {
        base.Exit();
        _SM.collectableTrigger.enabled = false;
        _SM.animator.SetBool("isCollectable", false);
    }

    public void SetCollected()
    {
        stateMachine.ChangeState(_SM.idleFollowState);
    }
}
