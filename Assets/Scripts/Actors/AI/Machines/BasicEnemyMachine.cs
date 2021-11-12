using System;
using UnityEngine;

public enum PatrolEndProtocol
{
    Stop,
    Loop,
    Mirror,
}
public enum PatrolReturnProtocol
{
    Start,
    End,
    Nearest,
    Continue,
}

/// Main behaviour handler for the Basic Enemy
[RequireComponent(typeof(Rigidbody2D))]
public class BasicEnemyMachine : StateMachine
{
    [SerializeField, Tooltip("Main animator for handling sprite changes.")]
    private Animator _Animator;
    public Animator animator {get {return _Animator;}}
    [SerializeField, Tooltip("Object to chase and attack when aggro.")]
    private GameObject _Target;
    public GameObject target {get {return _Target;}}

    [Serializable]
    public class StateParameters
    {
        [SerializeField, Tooltip("Distance from which this machine can see the target.")]
        private float _DetectionRadius = 5.0f;
        public float detectionRadius {get {return _DetectionRadius;}}

        [Serializable]
        public class Passive
        {
            [Serializable]
            public class Patrol
            {
                [SerializeField, DraggablePoint, Tooltip("Global positions of the patrol route (in order).")]
                public Vector3[] patrolPoints;

                [SerializeField, Tooltip("Speed to move whilst patrolling.")]
                private float _PatrolSpeed = 1.0f;
                public float patrolSpeed {get {return _PatrolSpeed;}}
                
                [Header("Patrol protocols")]
                [SerializeField, Tooltip("Patrol protocol this machine should carry out upon reaching the end of it's patrol route.\n"
                    + "• Stop - Enter the stopped state.\n"
                    + "• Loop - Return to the first position and continue.\n"
                    + "• Mirror - Flip patrol direction upon reaching the beginning/end of the route.")]
                private PatrolEndProtocol _PatrolEndProtocol = PatrolEndProtocol.Stop;
                public PatrolEndProtocol patrolEndProtocol {get {return _PatrolEndProtocol;}}
                [SerializeField, Tooltip("Patrol protocol this machine should carry out upon re-entering the patrol state.\n"
                    + "• Start - Return to the first point in the route and continue.\n"
                    + "• End - Return to the last point in the route and continue.\n"
                    + "• Nearest - Go to the nearest point and continue (TO BE IMPLEMENTED).\n"
                    + "• Continue - Continue from the last target position.")]
                private PatrolReturnProtocol _PatrolReturnProtocol = PatrolReturnProtocol.Start;
                public PatrolReturnProtocol patrolReturnProtocol {get {return _PatrolReturnProtocol;}}
            }
            [SerializeField]
            private Patrol _Patrol;
            public Patrol patrol {get {return _Patrol;}}

            [Serializable]
            public class Stopped
            {

            }
            [SerializeField]
            private Stopped _Stopped;
            public Stopped stopped {get {return _Stopped;}}
        }
        [SerializeField]
        private Passive _Passive;
        public Passive passive {get {return _Passive;}}

        [Serializable]
        public class Aggressive
        {
            [Serializable]
            public class Chase
            {
                [SerializeField, Tooltip("Speed to move whilst chasing.")]
                private float _ChaseSpeed = 1.5f;
                public float chaseSpeed {get {return _ChaseSpeed;}}
            }
            [SerializeField]
            private Chase _Chase;
            public Chase chase {get {return _Chase;}}

            [Serializable]
            public class Lunge
            {
                [SerializeField, Tooltip("Distance from which this machine will lunge at the target.")]
                private float _LungeRadius = 3.0f;
                public float lungeRadius {get {return _LungeRadius;}}
                [SerializeField, Tooltip("Time taken for this machine to windup before a lunge.")]
                private float _LungeWindupTime = 1.0f;
                public float lungeWindupTime {get {return _LungeWindupTime;}}
                [SerializeField, Tooltip("Time taken for this machine to recover after a lunge.")]
                private float _LungeCooldownTime = 1.0f;
                public float lungeCooldownTime {get {return _LungeCooldownTime;}}
                [SerializeField, Tooltip("Magnitude of the force to apply to the rigidbody when lunging.")]
                private float _LungeForce = 2.0f;
                public float lungeForce {get {return _LungeForce;}}
            }
            [SerializeField]
            private Lunge _Lunge;
            public Lunge lunge {get {return _Lunge;}}
        }
        [SerializeField]
        private Aggressive _Aggressive;
        public Aggressive aggressive {get {return _Aggressive;}}
    }

    [SerializeField]
    private StateParameters _StateParameters;
    public StateParameters stateParameters {get {return _StateParameters;}}

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
