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
    }

    public override void Enter()
    {
        base.Enter();
        _IsGrounded = _MoveParameters.groundCheck.IsTriggered();
        _WasGrounded = _IsGrounded;
        _DoJump = false;
        _IsRunning = false;
        _IsCrouched = false;
        movementSpeed = _MoveParameters.walkSpeed;

        _MoveParameters.standingCollider.enabled = true;
        _MoveParameters.crouchingCollider.enabled = false;
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
        _IsGrounded = _MoveParameters.groundCheck.IsTriggered();

        if (_DoJump)
        {
            _DoJump = false;
            if (_IsGrounded && _SinceLastJump > _MoveParameters.jumpDelay)
            {
                _SinceLastJump = 0.0f;
                _SM.rigidbody2D.AddForce(new Vector2(0, _MoveParameters.jumpForce));
                _SM.animator.SetTrigger("doJump");
                _SM.audioSource.PlayOneShot(_MoveParameters.jumpSound);
            }
        }

        if (_IsCrouched != _SM.crouchButtonDown || _IsRunning != _SM.runButtonDown)
        {
            if (_IsCrouched != _SM.crouchButtonDown)
            {
                _IsCrouched = _SM.crouchButtonDown || (!_SM.crouchButtonDown && _MoveParameters.ceilingCheck.IsTriggered());
                if (_IsCrouched)
                {
                    _MoveParameters.crouchingCollider.enabled = true;
                    _MoveParameters.standingCollider.enabled = false;
                }
                else
                {
                    _MoveParameters.standingCollider.enabled = true;
                    _MoveParameters.crouchingCollider.enabled = false;
                }
            }
            _IsRunning = _SM.runButtonDown;
            if (_IsCrouched)
                movementSpeed = _MoveParameters.walkSpeed * _MoveParameters.crouchSpeed;
            else if (_IsRunning)
                movementSpeed = _MoveParameters.walkSpeed * _MoveParameters.runSpeed;
            else
                movementSpeed = _MoveParameters.walkSpeed;
        }
        Vector3 targetVelocity = new Vector2(movementSpeed * 10f * _MoveDirection, _SM.rigidbody2D.velocity.y);
        _SM.rigidbody2D.velocity = Vector3.SmoothDamp(_SM.rigidbody2D.velocity, targetVelocity, ref _Velocity, _MoveParameters.movementSmoothing);


        if (_IsGrounded && !_WasGrounded)
        {
            _SM.animator.SetTrigger("doLand");
            _SM.audioSource.PlayOneShot(_MoveParameters.landSound);
            foreach (ParticleGenerator generator in _MoveParameters.landParticles)
                generator.GenerateParticle();
        }
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
