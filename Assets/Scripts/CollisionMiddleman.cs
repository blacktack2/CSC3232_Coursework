using UnityEngine;
using UnityEngine.Events;

public class CollisionMiddleman : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<Collision2D> _CollisionEvent;
    [SerializeField]
    private UnityEvent<Collider2D> _TriggerEvent;

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision " + other.gameObject.name);
        _CollisionEvent.Invoke(other);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger " + other.gameObject.name);
        _TriggerEvent.Invoke(other);
    }
}
