using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicSwordMachine : StateMachine
{
    [SerializeField]
    private Animator _Animator;
    public Animator animator {get {return _Animator;}}

    [SerializeField]
    private Collider2D _CollectableTrigger;
    public Collider2D collectableTrigger {get {return _CollectableTrigger;}}
    [SerializeField]
    private Collider2D[] _PrimaryAttackColliders;
    public Collider2D[] primaryAttackColliders {get {return _PrimaryAttackColliders;}}
    [SerializeField]
    private Collider2D[] _SecondaryAttackColliders;
    public Collider2D[] secondaryAttackColliders {get {return _SecondaryAttackColliders;}}

    [SerializeField]
    private float _HoverRange = 0.5f;
    [SerializeField]
    private float _FollowSpeed = 1.0f;
    public float followSpeed {get {return _FollowSpeed;}}
    [SerializeField]
    private float _HoverSpeed = 1.0f;
    public float hoverSpeed {get {return _HoverSpeed;}}
    public float hoverRange {get {return _HoverRange;}}

    private SwordWielder _Wielder;
    public SwordWielder wielder {get {return _Wielder;}}
    private Rigidbody2D _Rigidbody2D;
    new public Rigidbody2D rigidbody2D {get {return _Rigidbody2D;}}

    public bool isFacingRight = true;

    [HideInInspector]
    public BasicSwordCollectable collectableState;
    private BasicSwordIdle[] _IdleStates = new BasicSwordIdle[2];
    [HideInInspector]
    public BasicSwordIdleFollow idleFollowState {get {return (BasicSwordIdleFollow) _IdleStates[0];} set {_IdleStates[0] = value;}}
    [HideInInspector]
    public BasicSwordIdleHover idleHoverState {get {return (BasicSwordIdleHover) _IdleStates[1];} set {_IdleStates[1] = value;}}
    private BasicSwordAttack[] _AttackStates = new BasicSwordAttack[2];
    [HideInInspector]
    public BasicSwordAttackPrimary primaryAttackState {get {return (BasicSwordAttackPrimary) _AttackStates[0];} set {_AttackStates[0] = value;}}
    [HideInInspector]
    public BasicSwordAttackSecondary secondaryAttackState {get {return (BasicSwordAttackSecondary) _AttackStates[1];} set {_AttackStates[1] = value;}}

    protected override void Awake()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        collectableState = new BasicSwordCollectable(this);
        idleFollowState = new BasicSwordIdleFollow(this);
        idleHoverState = new BasicSwordIdleHover(this);
        primaryAttackState = new BasicSwordAttackPrimary(this);
        secondaryAttackState = new BasicSwordAttackSecondary(this);
        base.Awake();
    }

    protected override BaseState GetInitialState()
    {
        return collectableState;
    }

    public void SetWeilder(SwordWielder wielder)
    {
        _Wielder = wielder;
        if (currentState is BasicSwordCollectable)
            ((BasicSwordCollectable) currentState).SetCollected();
    }

    public void SetDirection(bool facingRight)
    {
        if (currentState is BasicSwordIdle)
            ((BasicSwordIdle) currentState).SetDirection(facingRight);
    }

    public void DoPrimaryAttack()
    {
        if (currentState is BasicSwordIdle)
            ((BasicSwordIdle) currentState).DoPrimaryAttack();
    }

    public void SetAttackCollider(int index)
    {
        if (currentState is BasicSwordAttack)
            ((BasicSwordAttack) currentState).SetAttackCollider(index);
    }

    public void DoSecondaryAttack()
    {
        if (currentState is BasicSwordIdle)
            ((BasicSwordIdle) currentState).DoSecondaryAttack();
    }

    public void DoEndAttack()
    {
        if (currentState is BasicSwordAttack)
            ((BasicSwordAttack) currentState).DoEndAttack();
    }

    public void AttackTriggered(Collider2D other, AttackTrigger trigger)
    {
        if (currentState is BasicSwordAttack)
            ((BasicSwordAttack) currentState).AttackTriggered(other, trigger);
    }
}