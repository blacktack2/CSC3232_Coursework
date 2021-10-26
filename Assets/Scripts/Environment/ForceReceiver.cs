using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D m_rigidbody2D;
    [SerializeField]
    private FragileObject fragileObject;

    [SerializeField]
    private bool isStatic = false;
    [SerializeField]
    private float staticBreakForce = 0.0f;

    void Awake()
    {
        if (isStatic)
            m_rigidbody2D.isKinematic = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        float impulse = 0.0f;

        foreach (ContactPoint2D point in other.contacts) {
            impulse += point.normalImpulse;
        }

        float force = impulse / Time.fixedDeltaTime;
        ForceApplied(force);
    }

    public void ApplyForce(Vector2 force)
    {
        if (!isStatic || force.magnitude >= staticBreakForce)
        {
            m_rigidbody2D.AddForce(force);
            ForceApplied(force.magnitude);
        }
    }

    private void ForceApplied(float magnitude)
    {
        if (fragileObject != null)
            fragileObject.ForceApplied(magnitude);

        if (!isStatic || magnitude >= staticBreakForce)
        {
            isStatic = false;
            m_rigidbody2D.isKinematic = false;
        }
    }
}
