using UnityEngine;
using UnityEngine.Events;

// Modified code sourced from: https://github.com/Brackeys/2D-Character-Controller/blob/master/CharacterController2D.cs
public class CharacterController2D : MonoBehaviour
{
	[SerializeField][Tooltip("Main rigidbody responsible for character physics.")]
	private Rigidbody2D _Rigidbody2D;
	[SerializeField][Tooltip("Force to apply to the rigidbody when a jump action is called.")]
    private float _JumpForce = 400f;
    [SerializeField][Tooltip("Time (seconds) until another jump action can be called.")]
    private float _JumpDelay = 0.2f;
	[Range(0, 1)] [SerializeField][Tooltip("Speed the character moves whilst crouching (in proportion to the normal movement speed)")]
    private float _CrouchSpeed = .36f;
	[Range(0, .3f)] [SerializeField]
    private float _MovementSmoothing = .05f;
	[SerializeField][Tooltip("Whether this character should be allowed control movement whilst airborn.")]
    private bool _AirControl = false;
	[SerializeField][Tooltip("Trigger used to define approximately when the character is touching the ground.")]
    private CollisionCheck _GroundCheck;
	[SerializeField][Tooltip("Trigger used to define approximately when the character should be touching the ceiling whilst idle.")]
    private CollisionCheck _CeilingCheck;
	[SerializeField][Tooltip("Collider to be present whilst in the idle state.")]
    private Collider2D _IdleCollider;
	[SerializeField][Tooltip("Collider to be present whilst in the crouched state.")]
    private Collider2D _CrouchCollider;

	private bool _IsGrounded;
	private bool _FacingRight = true;
	
	private Vector3 _Velocity = Vector3.zero;

    private float _SinceLastJump = 0.0f;

	[Header("Events")]
	[Space]

	[SerializeField]
	private UnityEvent _OnJumpEvent;
	[SerializeField]
	private UnityEvent _OnLandEvent;

	[System.Serializable]
	private class BoolEvent : UnityEvent<bool> { }

	[SerializeField]
	private BoolEvent _OnCrouchEvent;
	private bool _WasCrouching = false;

	private void Awake()
	{
		if (_OnJumpEvent == null)
			_OnJumpEvent = new UnityEvent();
		if (_OnLandEvent == null)
			_OnLandEvent = new UnityEvent();

		if (_OnCrouchEvent == null)
			_OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
        _SinceLastJump += Time.fixedDeltaTime;
		bool wasGrounded = _IsGrounded;
        _IsGrounded = _GroundCheck.IsTriggered();
		if (_IsGrounded && ! wasGrounded)
			_OnLandEvent.Invoke();
	}

	public void Move(float move, bool crouch, bool jump)
	{
		if (_WasCrouching && !crouch)
		{
            if (_CeilingCheck.IsTriggered())
			{
				crouch = true;
			}
		}

		if (_IsGrounded || _AirControl)
		{
			if (crouch)
			{
				if (!_WasCrouching)
				{
					_WasCrouching = true;
					_OnCrouchEvent.Invoke(true);
				}

				move *= _CrouchSpeed;

                _CrouchCollider.enabled = true;
                _IdleCollider.enabled = false;
			}
            else
			{
                _IdleCollider.enabled = true;
                _CrouchCollider.enabled = false;

				if (_WasCrouching)
				{
					_WasCrouching = false;
					_OnCrouchEvent.Invoke(false);
				}
			}

			Vector3 targetVelocity = new Vector2(move * 10f, _Rigidbody2D.velocity.y);
			_Rigidbody2D.velocity = Vector3.SmoothDamp(_Rigidbody2D.velocity, targetVelocity, ref _Velocity, _MovementSmoothing);

			if (move > 0 && !_FacingRight)
				Flip();
			else if (move < 0 && _FacingRight)
				Flip();
		}

		if (_IsGrounded && jump && _SinceLastJump >= _JumpDelay)
		{
            _SinceLastJump = 0.0f;
			_IsGrounded = false;
			_Rigidbody2D.AddForce(new Vector2(0f, _JumpForce));
			_OnJumpEvent.Invoke();
		}
	}

	private void Flip()
	{
		_FacingRight = !_FacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public bool IsGrounded()
	{
		return _IsGrounded;
	}
}