using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Modified code sourced from: https://github.com/Brackeys/2D-Character-Controller/blob/master/CharacterController2D.cs
public class CharacterController2D : MonoBehaviour
{
	[SerializeField]
    private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
    [SerializeField]
    private float jumpDelay = 0.2f;
	[Range(0, 1)] [SerializeField]
    private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField]
    private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField]
    private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField]
    private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField]
    private CollisionCheck m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField]
    private CollisionCheck m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField]
    private Collider2D IdleCollider;
	[SerializeField]
    private Collider2D CrouchCollider;
	[SerializeField]
	private GameObject sprite;

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
    private float lastJump = 0.0f;

	[Header("Events")]
	[Space]

	public UnityEvent OnJumpEvent;
	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnJumpEvent == null)
			OnJumpEvent = new UnityEvent();
		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
        lastJump += Time.fixedDeltaTime;
		bool wasGrounded = m_Grounded;
        m_Grounded = m_GroundCheck.IsTriggered();
		if (m_Grounded && ! wasGrounded)
			OnLandEvent.Invoke();
	}

	public void Move(float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		if (m_wasCrouching && !crouch)
		{
            if (m_CeilingCheck.IsTriggered())
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

                CrouchCollider.enabled = true;
                IdleCollider.enabled = false;
			}
            else
			{
            // Enable the collider when not crouching
                IdleCollider.enabled = true;
                CrouchCollider.enabled = false;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump && lastJump >= jumpDelay)
		{
			// Add a vertical force to the player.
            lastJump = 0.0f;
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			OnJumpEvent.Invoke();
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public bool IsGrounded()
	{
		return m_Grounded;
	}
}