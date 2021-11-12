using UnityEngine;
using UnityEngine.Events;

public class CollisionMiddleman : MonoBehaviour
{
    [SerializeField, Tooltip("Event to call when a collision occurs.")]
    private UnityEvent<Collision2D> _CollisionEvent;
    [SerializeField, Tooltip("Event to call when a trigger overlap occurs.")]
    private UnityEvent<Collider2D> _TriggerEvent;

    void OnCollisionEnter2D(Collision2D other)
    {
        _CollisionEvent.Invoke(other);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _TriggerEvent.Invoke(other);
    }
}
