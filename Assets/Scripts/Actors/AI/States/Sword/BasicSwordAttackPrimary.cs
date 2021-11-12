using UnityEngine;

public class BasicSwordAttackPrimary : BasicSwordAttack
{
    public BasicSwordAttackPrimary(BasicSwordMachine stateMachine) : base("AttackPrimary", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _SM.animator.SetTrigger("EnterPrimaryAttackState");
    }

    protected override Collider2D[] GetAttackColliders()
    {
        return _PrimaryParameters.primaryAttackColliders;
    }
}
