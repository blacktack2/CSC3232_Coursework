using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField]
    private Collider2D attackCollider;
    [SerializeField]
    private Renderer attackRenderer;

    [SerializeField]
    private Transform followPoint;
    [SerializeField]
    private Renderer followRenderer;

    [SerializeField]
    private float repeatDelay = 0.4f;

    [SerializeField]
    private float swingDelay = 0.1f;
    [SerializeField]
    private float swingDuration = 0.1f;

    private bool doSwing = false;
    private bool isSwinging = false;

    private float swingTime = 0.0f;
    private float sinceLastSwing = 0.0f;

    void Awake()
    {
        attackCollider.enabled = false;
        attackRenderer.enabled = false;

        followRenderer.enabled = true;
    }

    void Update()
    {
        if (Input.GetButtonUp("Attack"))
            doSwing = true;
    }

    void FixedUpdate()
    {
        sinceLastSwing += Time.fixedDeltaTime;
        if (isSwinging)
        {
            swingTime += Time.fixedDeltaTime;
            if (swingTime > swingDelay + swingDuration)
            {
                isSwinging = false;
                attackCollider.enabled = false;
                attackRenderer.enabled = false;
                followRenderer.enabled = true;
            }
            else if (swingTime > swingDelay)
            {
                attackCollider.enabled = true;
            }
        }

        if (doSwing)
        {
            doSwing = false;
            if (!isSwinging && sinceLastSwing > repeatDelay)
            {
                sinceLastSwing = 0.0f;
                isSwinging = true;
                swingTime = 0.0f;
                followRenderer.enabled = false;
                attackRenderer.enabled = true;
            }
        }
    }
}
