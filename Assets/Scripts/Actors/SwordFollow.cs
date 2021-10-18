using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFollow : MonoBehaviour
{
    [SerializeField]
    private Transform runtimeParent = null;
    [SerializeField]
    private Transform targetPoint;
    [SerializeField]
    private Rigidbody2D m_rigidbody2D;
    [SerializeField]
    private float speed = 1.0f;

    void Awake()
    {
        transform.SetParent(runtimeParent);
    }

    void FixedUpdate()
    {
        m_rigidbody2D.velocity = speed * (targetPoint.position - transform.position);
    }
}
