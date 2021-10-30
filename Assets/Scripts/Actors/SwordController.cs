using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField][Tooltip("The sword wielder this weapon is attached to if applicable.")]
    private SwordWielder _Wielder;
    [SerializeField][Tooltip("Main animator for this controller to set the parameters of to represent each state.")]
    private Animator _Animator;
    [SerializeField][Tooltip("Main rigidbody with which to move and reposition the sword.")]
    private Rigidbody2D _Rigidbody2D;

    [SerializeField][Tooltip("Transform for the rigidbody to move towards whilst idle. To be replaced with an offset parameter relative to the wielder.")]
    private Transform _FollowPoint;
    [SerializeField][Tooltip("Transform for this controller to exactly match whilst attacking. To be replaced with an offset parameter relative to the wielder.")]
    private Transform _AttackPoint;

    [SerializeField][Tooltip("Speed at which to follow the follow point during the idle-follow state.")]
    private float _FollowSpeed = 1.0f;
    [SerializeField][Tooltip("Distance from the follow point at which the sword should change to the idle-hover state.")]
    private float _FollowHoverRange = 0.5f;
    [SerializeField][Tooltip("Speed at which to randomly hover around the follow point during the idle-hover state")]
    private float _FollowHoverSpeed = 1.0f;
    
    [SerializeField][Tooltip("Delay (in seconds) before being allowed to attack again after a primary attack has finished.")]
    private float _PrimaryRepeatDelay = 1.0f;
    [SerializeField][Tooltip("Delay (in seconds) before beign allowed to attack again after a secondary attack has finished.")]
    private float _SecondaryRepeatDelay = 1.0f;

    [SerializeField][Tooltip("Force applied to force receivers when hit by a primary attack.")]
    private Vector2 _PrimaryForce;
    [SerializeField][Tooltip("Force applied to force receivers when hit by a secondary attack.")]
    private Vector2 _SecondaryForce;

    private float _RepeatDelay = 0.0f; // Delay until the controller is allowed to attack again

    private bool _DoPrimaryAttack = false;
    private bool IsDoingPrimaryAttack = false;
    private bool _DoSecondaryAttack = false;
    private bool _IsDoingSecondaryAttack = false;
    private bool _IsAttacking = false;

    private float _SinceLastAttack = 0.0f;

    private bool _IsFacingRight = true;

    private float _IdleRandomHoverSeed = 0;

    void Awake()
    {
        _IdleRandomHoverSeed = UnityEngine.Random.Range(-1000.0f, 1000.0f);
    }

    void FixedUpdate()
    {
        DoTransformActions();
        DoAttackChecks();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_IsAttacking)
        {
            ForceReceiver otherRigidbody = other.GetComponentInParent<ForceReceiver>();
            if (otherRigidbody != null)
            {
                if (IsDoingPrimaryAttack)
                    otherRigidbody.ApplyForce(_PrimaryForce * transform.localScale);
                else if (_IsDoingSecondaryAttack)
                    otherRigidbody.ApplyForce(_SecondaryForce * transform.localScale);
            }
        }
    }

    private void DoTransformActions()
    {
        if (_IsAttacking)
        {
            _Rigidbody2D.isKinematic = true;
            transform.position = _AttackPoint.position;
        }
        else
        {
            Vector2 delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            if (delta.x >= 0 && !_IsFacingRight)
            {
                Flip();
            }
            else if (delta.x < 0 && _IsFacingRight)
            {
                Flip();
            }
            
            _Rigidbody2D.isKinematic = false;
            if (Vector2.Distance(transform.position, _FollowPoint.position) > _FollowHoverRange)
            {
                _Rigidbody2D.velocity = _FollowSpeed * (_FollowPoint.position - transform.position);
            }
            else
            {
                _Rigidbody2D.velocity = _FollowHoverSpeed * (
                    _FollowPoint.position - transform.position +
                     new Vector3(Mathf.PerlinNoise(Time.time, _IdleRandomHoverSeed), Mathf.PerlinNoise(_IdleRandomHoverSeed, Time.time), 0.0f)
                      * _FollowHoverRange);
            }
        }
    }

    private void DoAttackChecks()
    {
        _SinceLastAttack += Time.fixedDeltaTime;

        if (_DoPrimaryAttack)
        {
            _DoPrimaryAttack = false;
            if (!_IsAttacking && _SinceLastAttack > _RepeatDelay)
            {
                _IsAttacking = true;
                IsDoingPrimaryAttack = true;
                _RepeatDelay = _PrimaryRepeatDelay;
                _Animator.SetTrigger("PrimaryAttack");
            }
        }

        if (_DoSecondaryAttack)
        {
            _DoSecondaryAttack = false;
            if (!_IsAttacking && _SinceLastAttack > _RepeatDelay)
            {
                _IsAttacking = true;
                _IsDoingSecondaryAttack = true;
                _RepeatDelay = _SecondaryRepeatDelay;
                _Animator.SetTrigger("SecondaryAttack");
            }
        }
    }

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		_IsFacingRight = !_IsFacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    public void SetWeilder(SwordWielder wielder, Transform followPoint, Transform attackPoint)
    {
        this._Wielder = wielder;
        this._FollowPoint = followPoint;
        this._AttackPoint = attackPoint;
    }

    public void DoPrimaryAttack()
    {
        _DoPrimaryAttack = true;
    }

    public void DoSecondaryAttack()
    {
        _DoSecondaryAttack = true;
    }

    public void SwingStopped()
    {
        _IsAttacking = false;
        IsDoingPrimaryAttack = false;
        _IsDoingSecondaryAttack = false;
        _SinceLastAttack = 0.0f;
    }
}
