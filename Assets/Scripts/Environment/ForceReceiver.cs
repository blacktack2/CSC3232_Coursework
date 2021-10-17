using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D m_rigidbody2D;
    [SerializeField]
    private float threshold = 0.0f;

    public bool ApplyForce(Vector2 force)
    {
        if (force.magnitude >= threshold)
        {
            m_rigidbody2D.AddForce(force);
            return true;
        }
        return false;
    }
}
