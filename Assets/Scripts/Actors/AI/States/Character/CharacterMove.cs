using UnityEngine;
using UnityEngine.Assertions;

public class CharacterMove : CharacterState
{

    private bool _DoJump = false;
    private bool _IsGrounded;
    private bool _WasGrounded;

    private float _SinceLastJump = 0.0f;
    private int _MoveDirection = 1;

    private bool _IsRunning;
    private bool _IsCrouched;

    private float movementSpeed;
    private Vector3 _Velocity;

    public CharacterMove(CharacterMachine stateMachine) : base("Move", stateMachine)
    {
        _IsGrounded = _SM.groundCheck.IsTriggered();
        _WasGrounded = _IsGrounded;
    }

    public override void Enter()
    {
        base.Enter();
        _DoJump = false;
        _IsRunning = false;
        _IsCrouched = false;
        movementSpeed = _SM.walkSpeed;

        _SM.standingCollider.enabled = true;
        _SM.crouchingCollider.enabled = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _SM.animator.SetInteger("motionVertical", (_SM.rigidbody2D.velocity.y < 0 ? -1 : (_SM.rigidbody2D.velocity.y > 0 ? 1 : 0)));
        _SM.animator.SetInteger("motionHorizontal", _MoveDirection);
        _SM.animator.SetBool("isRunning", _IsRunning);
        _SM.animator.SetBool("isCrouched", _IsCrouched);
        _SM.animator.SetBool("isGrounded", _IsGrounded);
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _SinceLastJump += Time.fixedDeltaTime;
        _IsGrounded = _SM.groundCheck.IsTriggered();

        if (_DoJump)
        {
            _DoJump = false;
            if (_IsGrounded && _SinceLastJump > _SM.jumpDelay)
            {
                _SinceLastJump = 0.0f;
                _SM.rigidbody2D.AddForce(new Vector2(0, _SM.jumpForce));
                _SM.animator.SetTrigger("doJump");
            }
        }

        if (_IsCrouched != _SM.crouchButtonDown || _IsRunning != _SM.runButtonDown)
        {
            if (_IsCrouched != _SM.crouchButtonDown)
            {
                _IsCrouched = _SM.crouchButtonDown || (!_SM.crouchButtonDown && _SM.ceilingCheck.IsTriggered());
                if (_IsCrouched)
                {
                    _SM.crouchingCollider.enabled = true;
                    _SM.standingCollider.enabled = false;
                }
                else
                {
                    _SM.standingCollider.enabled = true;
                    _SM.crouchingCollider.enabled = false;
                }
            }
            _IsRunning = _SM.runButtonDown;
            if (_IsCrouched)
                movementSpeed = _SM.walkSpeed * _SM.crouchSpeed;
            else if (_IsRunning)
                movementSpeed = _SM.walkSpeed * _SM.runSpeed;
            else
                movementSpeed = _SM.walkSpeed;
        }
        Vector3 targetVelocity = new Vector2(movementSpeed * 10f * _MoveDirection, _SM.rigidbody2D.velocity.y);
        _SM.rigidbody2D.velocity = Vector3.SmoothDamp(_SM.rigidbody2D.velocity, targetVelocity, ref _Velocity, _SM.movementSmoothing);


        if (_IsGrounded && !_WasGrounded)
            _SM.animator.SetTrigger("doLand");
        _WasGrounded = _IsGrounded;
    }

    public void SetDirection(int direction)
    {
        Assert.IsTrue(direction >= -1 && direction <= 1, "direction must be between -1 and 1");
        if (direction != _MoveDirection)
        {
            _MoveDirection = direction;
            if (direction != 0)
                _SM.transform.localScale = Vector3.Scale(_SM.initialScale, new Vector3(direction, 1, 1));
        }
    }

    public void DoJump()
    {
        _DoJump = true;
    }
}
