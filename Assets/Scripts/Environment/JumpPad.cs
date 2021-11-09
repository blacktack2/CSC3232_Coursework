using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField]
    private Vector2 _JumpForce = Vector2.zero;

    public void TriggerEntered(Collider2D other)
    {
        Rigidbody2D rigidbody2D = other.attachedRigidbody;
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
        rigidbody2D.AddForce(_JumpForce);
    }
}
