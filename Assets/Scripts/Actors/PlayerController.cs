using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterController2D controller;

    [SerializeField]
    private Rigidbody2D m_rigidbody2D;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float runSpeed = 40f;

    private float horizontalMove = 0f;
    private bool doJump = false;
    private bool doCrouch = false;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        horizontalMove = horizontal * runSpeed;

        doJump = Input.GetButton("Jump");
        doCrouch = Input.GetButton("Crouch");

        animator.SetInteger("motionHorizontal", (int) Mathf.Abs(horizontal));
        animator.SetFloat("motionVertical", m_rigidbody2D.velocity.y);
        animator.SetBool("isGrounded", controller.IsGrounded());
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("isCrouched", isCrouching);
    }

    public void OnJumping()
    {
        animator.SetTrigger("onJump");
    }

    public void OnLanding()
    {
        animator.SetTrigger("onLand");
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, doCrouch, doJump);
    }
}
