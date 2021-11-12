using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField, Tooltip("Rigidbody force is being applied to.")]
    private Rigidbody2D _Rigidbody2D;
    [SerializeField, Tooltip("Call force operations on the fragile object (if present) whenever a force is applied.")]
    private FragileObject _FragileObject;

    [SerializeField, Tooltip("Start this object as immobile until a force greater than or equal to the static break force is received.")]
    private bool _IsStatic = false;
    [SerializeField, Tooltip("Minimum force required to disable static mode if applicable.")]
    private float _StaticBreakForce = 0.0f;

    void Awake()
    {
        if (_IsStatic)
            _Rigidbody2D.isKinematic = true;
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
        if (!_IsStatic || force.magnitude >= _StaticBreakForce)
        {
            _Rigidbody2D.AddForce(force);
            ForceApplied(force.magnitude);
        }
    }

    private void ForceApplied(float magnitude)
    {
        if (_FragileObject != null)
            _FragileObject.ForceApplied(magnitude);

        if (!_IsStatic || magnitude >= _StaticBreakForce)
        {
            _IsStatic = false;
            _Rigidbody2D.isKinematic = false;
        }
    }
}
