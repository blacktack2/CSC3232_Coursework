using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField][Tooltip("Main character controller to be handled.")]
    private CharacterController2D _CharacterController;

    [SerializeField][Tooltip("Main rigidbody responsible for character physics.")]
    private Rigidbody2D _Rigidbody2D;

    [SerializeField][Tooltip("Main animator responsible for displaying the character.")]
    private Animator _Animator;

    [SerializeField][Tooltip("Speed the character should move whilst in the run state.")]
    private float _RunSpeed = 40f;

    private float _HorizontalMove = 0f;
    private bool _DoJump = false;
    private bool _DoCrouch = false;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        _HorizontalMove = horizontal * _RunSpeed;

        _DoJump = Input.GetButton("Jump");
        _DoCrouch = Input.GetButton("Crouch");

        _Animator.SetInteger("motionHorizontal", (int) Mathf.Abs(horizontal));
        _Animator.SetFloat("motionVertical", _Rigidbody2D.velocity.y);
        _Animator.SetBool("isGrounded", _CharacterController.IsGrounded());
    }

    void FixedUpdate()
    {
        _CharacterController.Move(_HorizontalMove * Time.fixedDeltaTime, _DoCrouch, _DoJump);
    }

    public void OnCrouching(bool isCrouching)
    {
        _Animator.SetBool("isCrouched", isCrouching);
    }

    public void OnJumping()
    {
        _Animator.SetTrigger("onJump");
    }

    public void OnLanding()
    {
        _Animator.SetTrigger("onLand");
    }
}
