using UnityEngine;

public class BasicEnemyLunge : BasicEnemyAggressive
{
    private Vector3 _LungeForce;
    private float _LungeWindup;
    private float _LungeCooldown;
    private bool _DoLunge;

    public BasicEnemyLunge(BasicEnemyMachine stateMachine) : base("Lunge", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _LungeForce = (_SM.target.transform.position - _SM.transform.position).normalized * _LungeParameters.lungeForce;
        _LungeWindup = 0.0f;
        _LungeCooldown = 0.0f;
        _SM.rigidbody2D.velocity = Vector3.zero;
        _DoLunge = true;

        _SM.animator.SetTrigger("doChargeup");
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _LungeWindup += Time.fixedDeltaTime;
        if (_LungeWindup > _LungeParameters.lungeWindupTime)
        {
            if (_DoLunge)
            {
                _DoLunge = false;
                _SM.rigidbody2D.AddForce(_LungeForce);
                _SM.animator.SetTrigger("doLunge");
            }
            else
            {
                _LungeCooldown += Time.fixedDeltaTime;
                if (_LungeCooldown > _LungeParameters.lungeCooldownTime)
                    stateMachine.ChangeState(_SM.chaseState);
            }
        }
        else
        {
            _SM.rigidbody2D.velocity = Vector3.zero;
        }
    }
}
