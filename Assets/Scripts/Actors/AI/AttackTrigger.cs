using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackTrigger : MonoBehaviour
{
    [SerializeField]
    private BasicSwordMachine _ParentMachine;

    [SerializeField]
    private Vector2 _AttackForce = Vector2.zero;
    public Vector2 attackForce {get {return _AttackForce;}}

    void Awake()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.isTrigger = true;
        collider.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _ParentMachine.AttackTriggered(other, this);
    }
}
