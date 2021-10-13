using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterController2D controller;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float runSpeed = 40f;

    private float horizontalMove = 0f;
    private bool doJump = false;
    private bool doCrouch = false;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        doJump = Input.GetButton("Jump");
        doCrouch = Input.GetButton("Crouch");
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("isCrouched", isCrouching);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, doCrouch, doJump);
    }
}
