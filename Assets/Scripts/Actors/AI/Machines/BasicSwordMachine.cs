using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicSwordMachine : StateMachine
{
    [SerializeField]
    private Animator _Animator;
    public Animator animator {get {return _Animator;}}

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

    private BasicSwordIdle[] idleStates = new BasicSwordIdle[2];
    [HideInInspector]
    public BasicSwordIdleFollow idleFollowState {get {return (BasicSwordIdleFollow) idleStates[0];} set {idleStates[0] = value;}}
    [HideInInspector]
    public BasicSwordIdleHover idleHoverState {get {return (BasicSwordIdleHover) idleStates[1];} set {idleStates[1] = value;}}
    private BasicSwordAttack[] attackStates = new BasicSwordAttack[2];
    [HideInInspector]
    public BasicSwordAttackPrimary primaryAttackState {get {return (BasicSwordAttackPrimary) attackStates[0];} set {attackStates[0] = value;}}
    [HideInInspector]
    public BasicSwordAttackSecondary secondaryAttackState {get {return (BasicSwordAttackSecondary) attackStates[1];} set {attackStates[1] = value;}}

    void Awake()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        idleFollowState = new BasicSwordIdleFollow(this);
        idleHoverState = new BasicSwordIdleHover(this);
        primaryAttackState = new BasicSwordAttackPrimary(this);
        secondaryAttackState = new BasicSwordAttackSecondary(this);
    }

    protected override BaseState GetInitialState()
    {
        return idleHoverState;
    }

    public void SetWeilder(SwordWielder wielder)
    {
        _Wielder = wielder;
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