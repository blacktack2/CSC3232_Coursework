using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField]
    private SwordWielder wielder;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Rigidbody2D m_rigidbody2D;

    [SerializeField]
    private LayerMask canTarget;

    [SerializeField]
    private Transform followPoint;
    [SerializeField]
    private Transform attackPoint;

    [SerializeField]
    private float followSpeed = 1.0f;
    [SerializeField]
    private float followHoverRange = 0.5f;
    [SerializeField]
    private float followHoverSpeed = 1.0f;
    
    [SerializeField]
    private float primaryRepeatDelay = 1.0f;
    [SerializeField]
    private float secondaryRepeatDelay = 1.0f;

    [SerializeField]
    private Vector2 primaryForce;
    [SerializeField]
    private Vector2 secondaryForce;

    private float repeatDelay = 0.0f;

    private bool doPrimaryAttack = false;
    private bool isDoingPrimary = false;
    private bool doSecondaryAttack = false;
    private bool isDoingSecondary = false;
    private bool isSwinging = false;

    private float sinceLastSwing = 0.0f;

    private bool facingRight = true;

    private float followHoverSeed = 0;

    void Awake()
    {
        followHoverSeed = UnityEngine.Random.Range(-1000.0f, 1000.0f);
    }

    void FixedUpdate()
    {
        DoTransformActions();
        DoAttackChecks();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isSwinging)
        {
            ForceReceiver otherRigidbody = other.GetComponentInParent<ForceReceiver>();
            if (otherRigidbody != null)
            {
                if (isDoingPrimary)
                    otherRigidbody.ApplyForce(primaryForce * transform.localScale);
                else if (isDoingSecondary)
                    otherRigidbody.ApplyForce(secondaryForce * transform.localScale);
            }
        }
    }

    private void DoTransformActions()
    {
        if (isSwinging)
        {
            m_rigidbody2D.isKinematic = true;
            transform.position = attackPoint.position;
        }
        else
        {
            Vector2 delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            if (delta.x >= 0 && !facingRight)
            {
                Flip();
            }
            else if (delta.x < 0 && facingRight)
            {
                Flip();
            }
            
            m_rigidbody2D.isKinematic = false;
            if (Vector2.Distance(transform.position, followPoint.position) > followHoverRange)
            {
                m_rigidbody2D.velocity = followSpeed * (followPoint.position - transform.position);
            }
            else
            {
                m_rigidbody2D.velocity = followHoverSpeed * (
                    followPoint.position - transform.position +
                     new Vector3(Mathf.PerlinNoise(Time.time, followHoverSeed), Mathf.PerlinNoise(followHoverSeed, Time.time), 0.0f)
                      * followHoverRange);
            }
        }
    }

    private void DoAttackChecks()
    {
        sinceLastSwing += Time.fixedDeltaTime;

        if (doPrimaryAttack)
        {
            doPrimaryAttack = false;
            if (!isSwinging && sinceLastSwing > repeatDelay)
            {
                isSwinging = true;
                isDoingPrimary = true;
                repeatDelay = primaryRepeatDelay;
                animator.SetTrigger("PrimaryAttack");
            }
        }

        if (doSecondaryAttack)
        {
            doSecondaryAttack = false;
            if (!isSwinging && sinceLastSwing > repeatDelay)
            {
                isSwinging = true;
                isDoingSecondary = true;
                repeatDelay = secondaryRepeatDelay;
                animator.SetTrigger("SecondaryAttack");
            }
        }
    }

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    public void SetWeilder(SwordWielder wielder, Transform followPoint, Transform attackPoint)
    {
        this.wielder = wielder;
        this.followPoint = followPoint;
        this.attackPoint = attackPoint;
    }

    public void DoPrimaryAttack()
    {
        doPrimaryAttack = true;
    }

    public void DoSecondaryAttack()
    {
        doSecondaryAttack = true;
    }

    public void SwingStopped()
    {
        isSwinging = false;
        isDoingPrimary = false;
        isDoingSecondary = false;
        sinceLastSwing = 0.0f;
    }
}
