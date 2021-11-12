using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField, Tooltip("Door to open when collected.")]
    private Door _ToOpen;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody.gameObject.CompareTag("Player"))
        {
            _ToOpen.Open();
            Destroy(gameObject);
        }
    }
}
