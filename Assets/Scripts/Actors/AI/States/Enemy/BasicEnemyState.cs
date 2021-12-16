
using UnityEngine;

using Pathfinding;

public class BasicEnemyState : BaseState
{
    protected BasicEnemyMachine _SM;
    protected BasicEnemyMachine.StateParameters _StateParameters;
    protected BasicEnemyMachine.StateParameters.Passive _PassiveParameters;
    protected BasicEnemyMachine.StateParameters.Passive.Patrol _PatrolParameters;
    protected BasicEnemyMachine.StateParameters.Passive.Stopped _StoppedParameters;
    protected BasicEnemyMachine.StateParameters.Aggressive _AggressiveParameters;
    protected BasicEnemyMachine.StateParameters.Aggressive.Search _SearchParameters;
    protected BasicEnemyMachine.StateParameters.Aggressive.Chase _ChaseParameters;

    private Vector3 _LastPos;
    private Vector3 _InitialScale;
    private Vector3 _FlippedScale;
    private bool _FacingRight = false;

    protected Path _Path;
    protected int _CurrentWaypoint = 0;
    protected bool _ReachedEndOfPath = false;
    protected bool _PathSet = false; // False until the seeker generates a new path

    public BasicEnemyState(string name, BasicEnemyMachine stateMachine) : base(name, stateMachine)
    {
        _SM = stateMachine;
        _StateParameters = _SM.stateParameters;
        _PassiveParameters = _StateParameters.passive;
        _PatrolParameters = _PassiveParameters.patrol;
        _StoppedParameters = _PassiveParameters.stopped;
        _AggressiveParameters = _StateParameters.aggressive;
        _SearchParameters = _AggressiveParameters.search;
        _ChaseParameters = _AggressiveParameters.chase;

        _LastPos = _SM.transform.position;
        _InitialScale = _SM.transform.localScale;
        _FlippedScale = Vector3.Scale(_InitialScale, new Vector3(-1, 1, 1));
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        Vector3 currentPos = _SM.transform.position;
        if (_FacingRight && currentPos.x < _LastPos.x)
        {
            _SM.transform.localScale = _InitialScale;
            _FacingRight = false;
        }
        else if (!_FacingRight && currentPos.x > _LastPos.x)
        {
            _SM.transform.localScale = _FlippedScale;
            _FacingRight = true;
        }
        _LastPos = currentPos;
    }

    protected void PathFind(float speed)
    {
        if (_Path != null && _PathSet)
        {
            if (_CurrentWaypoint < _Path.vectorPath.Count)
            {
                Vector2 direction = ((Vector2) _Path.vectorPath[_CurrentWaypoint] - _SM.rigidbody2D.position).normalized;
                Vector2 force = direction * speed * Time.fixedDeltaTime;

                _SM.rigidbody2D.AddForce(force);

                float distance = Vector2.Distance(_SM.rigidbody2D.position, _Path.vectorPath[_CurrentWaypoint]);
                if (distance < _StateParameters.waypointThreshold)
                    _CurrentWaypoint++;
            }
        }
    }

    protected void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _Path = p;
            _CurrentWaypoint = 0;
            _PathSet = true;
        }
    }

    public virtual void UpdatePath()
    {
        _SM.seeker.StartPath(_SM.rigidbody2D.position, _SM.rigidbody2D.position, OnPathComplete);
    }

    protected bool CanSeeTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(_SM.transform.position, (_SM.target.transform.position - _SM.transform.position).normalized,
            _SM.stateParameters.detectionRadius, _SM.canSee);
        if (hit.collider != null)
        {
            GameObject other;
            if (hit.collider.attachedRigidbody != null)
                other = hit.collider.attachedRigidbody.gameObject;
            else
                other = hit.collider.gameObject;
            if (GameObject.ReferenceEquals(other, _SM.target))
                return true;
        }
        return false;
    }
}
