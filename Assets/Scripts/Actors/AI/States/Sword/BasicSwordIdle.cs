using UnityEngine;

public class BasicSwordIdle : BasicSwordState
{
    private bool _DoPrimaryAttack;
    private bool _DoSecondaryAttack;

    public BasicSwordIdle(string name, BasicSwordMachine stateMachine) : base(name, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _DoPrimaryAttack = false;
        _DoSecondaryAttack = false;

        _SM.animator.SetTrigger("EnterIdleState");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (_DoPrimaryAttack)
        {
            stateMachine.ChangeState(_SM.primaryAttackState);
        } else if (_DoSecondaryAttack)
        {
            stateMachine.ChangeState(_SM.secondaryAttackState);
        }
    }

    public void DoPrimaryAttack()
    {
        _DoPrimaryAttack = true;
    }

    public void DoSecondaryAttack()
    {
        _DoSecondaryAttack = true;
    }

    public void SetDirection(bool facingRight)
    {
        if (_SM.isFacingRight != facingRight)
        {
            _SM.isFacingRight = facingRight;
            _SM.transform.localScale = Vector3.Scale(_SM.transform.localScale, new Vector3(-1, 1, 1));
        }
    }
}
