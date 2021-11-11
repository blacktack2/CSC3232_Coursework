using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PatrolEndProtocol
{
    Stop,
    Loop,
    Mirror
}
public enum PatrolReturnProtocol
{
    Start,
    End,
    Nearest,
    Continue,
}

[RequireComponent(typeof(Rigidbody2D))]
public class BasicEnemyMachine : StateMachine
{
    [SerializeField]
    private Animator _Animator;
    public Animator animator {get {return _Animator;}}
    [SerializeField]
    private GameObject _Target;
    public GameObject target {get {return _Target;}}

    [SerializeField]
    private PatrolEndProtocol _PatrolEndProtocol = PatrolEndProtocol.Stop;
    public PatrolEndProtocol patrolEndProtocol {get {return _PatrolEndProtocol;}}
    [SerializeField]
    private PatrolReturnProtocol _PatrolReturnProtocol = PatrolReturnProtocol.Start;
    public PatrolReturnProtocol patrolReturnProtocol {get {return _PatrolReturnProtocol;}}

    [SerializeField][DraggablePoint]
    public Vector3[] patrolPoints;

    [SerializeField]
    private float _PatrolSpeed = 1.0f;
    public float patrolSpeed {get {return _PatrolSpeed;}}
    [SerializeField]
    private float _ChaseSpeed = 1.5f;
    public float chaseSpeed {get {return _ChaseSpeed;}}
    [SerializeField]
    private float _LungeForce = 2.0f;
    public float lungeForce {get {return _LungeForce;}}

    [SerializeField]
    private float _DetectionRadius = 5.0f;
    public float detectionRadius {get {return _DetectionRadius;}}
    [SerializeField]
    private float _LungeRadius = 3.0f;
    public float lungeRadius {get {return _LungeRadius;}}
    [SerializeField]
    private float _LungeWindupTime = 1.0f;
    public float lungeWindupTime {get {return _LungeWindupTime;}}
    [SerializeField]
    private float _LungeRecoverTime = 1.0f;
    public float lungeRecoverTime {get {return _LungeRecoverTime;}}

    private Rigidbody2D _Rigidbody2D;
    new public Rigidbody2D rigidbody2D {get {return _Rigidbody2D;}}

    private BasicEnemyPassive[] _PassiveStates = new BasicEnemyPassive[2];
    [HideInInspector]
    public BasicEnemyPatrol patrolState {get {return (BasicEnemyPatrol) _PassiveStates[0];} set {_PassiveStates[0] = value;}}
    [HideInInspector]
    public BasicEnemyStopped stoppedState {get {return (BasicEnemyStopped) _PassiveStates[1];} set {_PassiveStates[1] = value;}}
    private BasicEnemyAggressive[] _AggressiveStates = new BasicEnemyAggressive[2];
    [HideInInspector]
    public BasicEnemyChase chaseState {get {return (BasicEnemyChase) _AggressiveStates[0];} set {_AggressiveStates[0] = value;}}
    [HideInInspector]
    public BasicEnemyLunge lungeState {get {return (BasicEnemyLunge) _AggressiveStates[1];} set {_AggressiveStates[1] = value;}}

    protected override void Awake()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        patrolState = new BasicEnemyPatrol(this);
        stoppedState = new BasicEnemyStopped(this);
        chaseState = new BasicEnemyChase(this);
        lungeState = new BasicEnemyLunge(this);
        base.Awake();
    }

    protected override BaseState GetInitialState()
    {
        return patrolState;
    }
}
