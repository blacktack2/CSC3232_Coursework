using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSwordAnimHandler : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SwordController controller;

    [SerializeField]
    private float idleFlashDelay = 1.0f;
    [SerializeField]
    private string idleFlashTrigger;

    [SerializeField]
    private GameObject sideSwingCollider;
    [SerializeField]
    private GameObject upSwingCollider1;
    [SerializeField]
    private GameObject upSwingCollider2;

    private float idleFlashCounter = 0.0f;

    void Awake()
    {
        sideSwingCollider.SetActive(false);
        upSwingCollider1.SetActive(false);
        upSwingCollider2.SetActive(false);
    }

    void Update()
    {
        idleFlashCounter += Time.deltaTime;
    }

    public void IdleFlashEvent()
    {
        if (idleFlashCounter >= idleFlashDelay)
        {
            idleFlashCounter = 0.0f;
            animator.SetTrigger(idleFlashTrigger);
        }
    }

    public void SetSideSwingCollider()
    {
        sideSwingCollider.SetActive(true);
    }

    public void StopSideSwingColliders()
    {
        sideSwingCollider.SetActive(false);
    }

    public void EndSideSwing()
    {
        controller.SwingStopped();
    }

    public void SetUpSwingCollider1()
    {
        upSwingCollider1.SetActive(true);
    }

    public void SetUpSwingCollider2()
    {
        upSwingCollider1.SetActive(false);
        upSwingCollider2.SetActive(true);
    }

    public void StopUpSwingColliders()
    {
        upSwingCollider1.SetActive(false);
        upSwingCollider2.SetActive(false);
    }

    public void EndUpSwing()
    {
        controller.SwingStopped();
    }
}
