using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSwordAttackSecondary : BasicSwordAttack
{
    public BasicSwordAttackSecondary(BasicSwordMachine stateMachine) : base("AttackSecondary", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _SM.animator.SetTrigger("EnterSecondaryAttackState");
    }

    protected override Collider2D[] GetAttackColliders()
    {
        return _SM.secondaryAttackColliders;
    }
}
