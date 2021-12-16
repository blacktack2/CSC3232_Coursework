using UnityEngine;

public class BasicEnemyStopped : BasicEnemyPassive
{
    public BasicEnemyStopped(BasicEnemyMachine stateMachine) : base("Stopped", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _SM.rigidbody2D.velocity = Vector3.zero;
    }
}
