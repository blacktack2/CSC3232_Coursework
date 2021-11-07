using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSwordIdleHover : BasicSwordIdle
{
    private float _HoverSeed = 0.0f;
    public BasicSwordIdleHover(BasicSwordMachine stateMachine) : base("IdleHover", stateMachine) {
        _HoverSeed = UnityEngine.Random.Range(-1000.0f, 1000.0f);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (Vector2.Distance(_SM.wielder.transform.position, _SM.transform.position) > _SM.hoverRange)
        {
            stateMachine.ChangeState(_SM.idleFollowState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _SM.rigidbody2D.velocity = _SM.hoverSpeed * (
                    _SM.wielder.transform.position - _SM.transform.position +
                     new Vector3(Mathf.PerlinNoise(Time.time, _HoverSeed), Mathf.PerlinNoise(_HoverSeed, Time.time), 0.0f)
                      * _SM.hoverRange);
    }
}
