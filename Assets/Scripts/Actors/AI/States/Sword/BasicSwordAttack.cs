using UnityEngine;

public abstract class BasicSwordAttack : BasicSwordState
{
    private bool _DoEndAttack;
    private int _CurrentlyActiveCollider = -1;

    public BasicSwordAttack(string name, BasicSwordMachine stateMachine) : base(name, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _DoEndAttack = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (_DoEndAttack)
            stateMachine.ChangeState(_SM.idleHoverState);
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _SM.transform.position = _SM.wielder.transform.position;
    }

    public void SetAttackCollider(int index)
    {
        Collider2D[] colliders = GetAttackColliders();
        if (_CurrentlyActiveCollider != -1)
            colliders[_CurrentlyActiveCollider].enabled = false;
        if (index < colliders.Length)
        {
            _CurrentlyActiveCollider = index;
            colliders[index].enabled = true;
        }
    }

    public void DoEndAttack()
    {
        SetAttackCollider(1000); // Set to an unreasonably high value to ensure leftover colliders are disabled
        _DoEndAttack = true;
    }

    public void AttackTriggered(Collider2D other, AttackTrigger trigger)
    {
        Rigidbody2D otherRigidbody = other.attachedRigidbody;
        if (otherRigidbody != null)
        {
            ForceReceiver otherForceReceiver = otherRigidbody.GetComponent<ForceReceiver>();
            if (otherForceReceiver != null)
            {
                Vector2 attackForce = new Vector2(trigger.attackForce.x * (_SM.isFacingRight ? 1 : -1), trigger.attackForce.y);
                otherForceReceiver.ApplyForce(attackForce);
            }
        }
    }

    protected abstract Collider2D[] GetAttackColliders();
}
