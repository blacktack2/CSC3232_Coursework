using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField]
    private Collider2D attackCollider;

    [SerializeField]
    private float swingDelay = 0.1f;
    [SerializeField]
    private float swingDuration = 0.1f;

    private bool doSwing = false;
    private bool isSwinging = false;

    private float swingTime = 0.0f;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonUp("Attack"))
            doSwing = true;
    }

    void FixedUpdate()
    {
        if (isSwinging)
        {
            swingTime += Time.fixedDeltaTime;
            if (swingTime > swingDelay + swingDuration)
            {
                isSwinging = false;
                attackCollider.enabled = false;
            }
            else if (swingTime > swingDelay)
            {
                attackCollider.enabled = true;
            }
        }

        if (doSwing)
        {
            doSwing = false;
            isSwinging = true;
            swingTime = 0.0f;
        }
    }
}
