using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMachine : StateMachine
{
    [SerializeField]
    private Animator _Animator;
    public Animator animator {get {return _Animator;}}

    [SerializeField]
    private CollisionCheck _GroundCheck;
    public CollisionCheck groundCheck {get {return _GroundCheck;}}
    [SerializeField]
    private CollisionCheck _CeilingCheck;
    public CollisionCheck ceilingCheck {get {return _CeilingCheck;}}
    [SerializeField]
    private Collider2D _StandingCollider;
    public Collider2D standingCollider {get {return _StandingCollider;}}
    [SerializeField]
    private Collider2D _CrouchingCollider;
    public Collider2D crouchingCollider {get {return _CrouchingCollider;}}

    [SerializeField]
    private float _WalkSpeed = 1.0f;
    public float walkSpeed {get {return _WalkSpeed;}}
    [SerializeField, Range(1.0f, Mathf.Infinity)]
    private float _RunSpeed = 2.0f;
    public float runSpeed {get {return _RunSpeed;}}
    [SerializeField, Range(0.0f, 1.0f)]
    private float _CrouchSpeed = 0.5f;
    public float crouchSpeed {get {return _CrouchSpeed;}}
    [SerializeField]
    private float _JumpForce = 100.0f;
    public float jumpForce {get {return _JumpForce;}}

    [SerializeField]
    private float _JumpDelay;
    public float jumpDelay {get {return _JumpDelay;}}
    [SerializeField]
    private float _MovementSmoothing;
    public float movementSmoothing {get {return _MovementSmoothing;}}

    private Rigidbody2D _Rigidbody2D;
    new public Rigidbody2D rigidbody2D {get {return _Rigidbody2D;}}

    private Vector3 _InitialScale;
    public Vector3 initialScale {get {return _InitialScale;}}

    private bool _CrouchButtonDown = false;
    public bool crouchButtonDown {get {return _CrouchButtonDown;}}
    private bool _RunButtonDown = false;
    public bool runButtonDown {get {return _RunButtonDown;}}

    private CharacterMove _MoveState;
    public CharacterMove moveState {get {return _MoveState;}}

    void Awake()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        _MoveState = new CharacterMove(this);

        _InitialScale = transform.localScale;
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
