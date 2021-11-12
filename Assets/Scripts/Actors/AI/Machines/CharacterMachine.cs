using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMachine : StateMachine
{
    [SerializeField, Tooltip("Main animator for handling sprite changes.")]
    private Animator _Animator;
    public Animator animator {get {return _Animator;}}

    [Serializable]
    public class StateParameters
    {
        [Serializable]
        public class Move
        {
            [SerializeField, Tooltip("Trigger used to determine when the character is touching the ground.")]
            private CollisionCheck _GroundCheck;
            public CollisionCheck groundCheck {get {return _GroundCheck;}}
            [SerializeField, Tooltip("Trigger used to determine when the character should be touching the ceiling whilst standing.")]
            private CollisionCheck _CeilingCheck;
            public CollisionCheck ceilingCheck {get {return _CeilingCheck;}}
            [SerializeField, Tooltip("Collider to enable whist character is standing.")]
            private Collider2D _StandingCollider;
            public Collider2D standingCollider {get {return _StandingCollider;}}
            [SerializeField, Tooltip("Collider to enable whilst character is crouching.")]
            private Collider2D _CrouchingCollider;
            public Collider2D crouchingCollider {get {return _CrouchingCollider;}}

            [SerializeField, Tooltip("Default walking speed.")]
            private float _WalkSpeed = 1.0f;
            public float walkSpeed {get {return _WalkSpeed;}}
            [SerializeField, Tooltip("Running speed, relative to the walking speed.")]
            private float _RunSpeed = 2.0f;
            public float runSpeed {get {return _RunSpeed;}}
            [SerializeField, Tooltip("Crawl speed, relative to the walking speed.")]
            private float _CrouchSpeed = 0.5f;
            public float crouchSpeed {get {return _CrouchSpeed;}}
            [SerializeField, Tooltip("Vertical force applied when jumping.")]
            private float _JumpForce = 100.0f;
            public float jumpForce {get {return _JumpForce;}}

            [SerializeField, Tooltip("Minimum time between jumps (should be non-zero to avoid multiple jumps before airborne)")]
            private float _JumpDelay;
            public float jumpDelay {get {return _JumpDelay;}}
            [SerializeField, Tooltip("Degree to which the horizontal character motion should be smoothed.")]
            private float _MovementSmoothing;
            public float movementSmoothing {get {return _MovementSmoothing;}}
        }
        [SerializeField]
        private Move _Move;
        public Move move {get {return _Move;}}
    }
    [SerializeField]
    private StateParameters _StateParameters;
    public StateParameters stateParameters {get {return _StateParameters;}}

    private Rigidbody2D _Rigidbody2D;
    new public Rigidbody2D rigidbody2D {get {return _Rigidbody2D;}}

    private Vector3 _InitialScale; // Used for flipping the player horizontally (scale is not expected to change in size)
    public Vector3 initialScale {get {return _InitialScale;}}

    private bool _CrouchButtonDown = false;
    public bool crouchButtonDown {get {return _CrouchButtonDown;}}
    private bool _RunButtonDown = false;
    public bool runButtonDown {get {return _RunButtonDown;}}

    private CharacterMove _MoveState;
    public CharacterMove moveState {get {return _MoveState;}}

    protected override void Awake()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        _MoveState = new CharacterMove(this);

        _InitialScale = transform.localScale;
        base.Awake();
    }

    protected override BaseState GetInitialState()
    {
        return moveState;
    }

    public void DoJump()
    {
        if (currentState is CharacterMove)
            ((CharacterMove) currentState).DoJump();
    }

    public void SetRunButtonDown(bool isDown)
    {
        _RunButtonDown = isDown;
    }

    public void SetCrouchButtonDown(bool isDown)
    {
        _CrouchButtonDown = isDown;
    }

    public void SetDirection(int direction)
    {
        if (currentState is CharacterMove)
            ((CharacterMove) currentState).SetDirection(direction);
    }
}
