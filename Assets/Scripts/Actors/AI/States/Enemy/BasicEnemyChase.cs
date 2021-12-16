using UnityEngine;

public class BasicEnemyChase : BasicEnemyAggressive
{
    public BasicEnemyChase(BasicEnemyMachine stateMachine) : base("Chase", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _SM.animator.SetTrigger("doChase");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (CanSeeTarget())
            _SM.lastKnownTargetPosition = _SM.target.transform.position;
        else
            _SM.ChangeState(_SM.searchState);
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        PathFind(_ChaseParameters.chaseSpeed);
    }

    public override void UpdatePath()
    {
        _SM.seeker.StartPath(_SM.rigidbody2D.position, _SM.target.transform.position, OnPathComplete);
    }
}
