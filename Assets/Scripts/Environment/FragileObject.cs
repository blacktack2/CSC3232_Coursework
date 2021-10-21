using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileObject : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D m_rigidbody2d;
    [SerializeField]
    private float breakingForce = 1.0f;
    [SerializeField]
    private bool isFragile = true;

    public void ForceApplied(float magnitude)
    {
        if (isFragile && magnitude >= breakingForce)
            Destroy(gameObject);
    }
}
