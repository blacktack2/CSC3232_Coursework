using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ForceReceiver : MonoBehaviour
{
    [SerializeField, Tooltip("Rigidbody force is being applied to.")]
    private Rigidbody2D _Rigidbody2D;

    [SerializeField, Tooltip("Start this object as immobile until a force greater than or equal to the static break force is received.")]
    private bool _IsStatic = false;
    [SerializeField, Tooltip("Minimum force required to disable static mode if applicable.")]
    private float _StaticBreakForce = 0.0f;

    [SerializeField, Tooltip("True if this object should be destroyed when receiving a force greater than the breaking force.")]
    private bool _IsFragile = true;
    [SerializeField, Tooltip("Force required to break this object if is fragile.")]
    private float _BreakingForce = 1.0f;

    [Serializable]
    public struct Sounds
    {
        public AudioClip hit;
        public AudioClip destroy;
    }
    [SerializeField]
    private Sounds _Sounds;
    [SerializeField]
    private float _HitSoundDelay = 0.1f;

    private AudioSource _AudioSource;

    private float _HitSoundTime = 0.0f;

    void Awake()
    {
        _AudioSource = GetComponent<AudioSource>();
        if (_IsStatic)
            _Rigidbody2D.isKinematic = true;
    }

    void Update()
    {
        _HitSoundTime += Time.deltaTime;
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
        if (_IsFragile && magnitude >= _BreakingForce)
        {
            if (_Sounds.destroy != null)
                _AudioSource.PlayOneShot(_Sounds.destroy);
            Destroy(gameObject);
            return;
        }

        if (!_IsStatic || magnitude >= _StaticBreakForce)
        {
            _IsStatic = false;
            _Rigidbody2D.isKinematic = false;
            if (_Sounds.hit != null && _HitSoundTime >= _HitSoundDelay)
            {
                _HitSoundTime = 0.0f;
                _AudioSource.PlayOneShot(_Sounds.hit);
            }
        }
    }
}
