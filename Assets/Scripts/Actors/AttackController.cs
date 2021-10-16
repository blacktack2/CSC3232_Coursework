using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField]
    private Vector2 attackForce;
    [SerializeField]
    private Transform director;

    private GlobalConstants gc;

    void Awake()
    {
        gc = GlobalConstants.GetInstance();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ForceReceiver receiver = other.GetComponent<ForceReceiver>();
        if (receiver != null)
        {
            receiver.ApplyForce(attackForce * director.localScale);
        }
    }
}
