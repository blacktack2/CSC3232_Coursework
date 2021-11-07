using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSwordIdleFollow : BasicSwordIdle
{
    public BasicSwordIdleFollow(BasicSwordMachine stateMachine) : base("IdleFollow", stateMachine) {
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (Vector2.Distance(_SM.wielder.transform.position, _SM.transform.position) < _SM.hoverRange)
        {
            stateMachine.ChangeState(_SM.idleHoverState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _SM.rigidbody2D.velocity = _SM.followSpeed * (_SM.wielder.transform.position - _SM.transform.position);
    }
}
