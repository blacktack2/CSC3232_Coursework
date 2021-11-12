using UnityEngine;

public class BasicEnemyChase : BasicEnemyAggressive
{
    public BasicEnemyChase(BasicEnemyMachine stateMachine) : base("Chase", stateMachine)
    {
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        float distance = Vector3.Distance(_SM.transform.position, _SM.target.transform.position);
        if (distance < _AggressiveParameters.lunge.lungeRadius)
            stateMachine.ChangeState(_SM.lungeState);
        else if (distance > _SM.stateParameters.detectionRadius)
            stateMachine.ChangeState(_SM.patrolState);
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _SM.rigidbody2D.velocity = (_SM.target.transform.position - _SM.transform.position).normalized * _ChaseParameters.chaseSpeed;
    }
}
