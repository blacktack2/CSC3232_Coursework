using UnityEngine;

public class BasicEnemySearch : BasicEnemyAggressive
{
    private Vector2 _Destination;
    private float _SearchTime = 0.0f;

    public BasicEnemySearch(BasicEnemyMachine stateMachine) : base("Search", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _Destination = _SM.lastKnownTargetPosition;
        _SearchTime = 0.0f;
        _SM.animator.SetTrigger("doSearch");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _SearchTime += Time.deltaTime;
        if (CanSeeTarget())
            _SM.ChangeState(_SM.chaseState);
        else if (_SearchTime > _SearchParameters.searchDuration)
            _SM.ChangeState(_SM.patrolState);
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        PathFind(_SearchParameters.searchSpeed);
        if (!(_Path != null && _PathSet && _CurrentWaypoint < _Path.vectorPath.Count))
            _Destination = AstarPath.active.GetNearest(_SM.lastKnownTargetPosition + Random.insideUnitCircle * _SearchParameters.searchRadius).position;
    }

    public override void UpdatePath()
    {
        _SM.seeker.StartPath(_SM.rigidbody2D.position, _Destination, OnPathComplete);
    }
}
