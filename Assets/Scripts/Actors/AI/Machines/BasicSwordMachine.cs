using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AudioSource))]
public class BasicSwordMachine : StateMachine
{
    [SerializeField, Tooltip("Main animator for handling sprite changes.")]
    private Animator _Animator;
    public Animator animator {get {return _Animator;}}

    [Serializable]
    public class StateParameters
    {
        [Serializable]
        public class Collectable
        {
            [SerializeField, Tooltip("Trigger to enable during the collectable state.")]
            private Collider2D _CollectableTrigger;
            public Collider2D collectableTrigger {get {return _CollectableTrigger;}}
        }
        [SerializeField]
        private Collectable _Collectable;
        public Collectable collectable {get {return _Collectable;}}

        [Serializable]
        public class Idle
        {
            [Serializable]
            public class Follow
            {
                [SerializeField, Tooltip("Speed to move whilst following the wielder.")]
                private float _FollowSpeed = 1.0f;
                public float followSpeed {get {return _FollowSpeed;}}
            }
            [SerializeField]
            private Follow _Follow;
            public Follow follow {get {return _Follow;}}

            [Serializable]
            public class Hover
            {
                [SerializeField, Tooltip("Range from wielder to stop following and enter the hover state.")]
                private float _HoverRange = 0.5f;
                public float hoverRange {get {return _HoverRange;}}
                [SerializeField, Tooltip("Speed to move whilst hovering.")]
                private float _HoverSpeed = 1.0f;
                public float hoverSpeed {get {return _HoverSpeed;}}
            }
            [SerializeField]
            private Hover _Hover;
            public Hover hover {get {return _Hover;}}
        }
        [SerializeField]
        private Idle _Idle;
        public Idle idle {get {return _Idle;}}

        [Serializable]
        public class Attack
        {
            [Serializable]
            public class Primary
            {
                [SerializeField, Tooltip("Ordered colliders for the primary attack.")]
                private Collider2D[] _PrimaryAttackColliders;
                public Collider2D[] primaryAttackColliders {get {return _PrimaryAttackColliders;}}
                
                [SerializeField]
                private ParticleGenerator _PrimaryAttackParticles;
                public ParticleGenerator primaryAttackParticles {get {return _PrimaryAttackParticles;}}
            }
            [SerializeField]
            private Primary _Primary;
            public Primary primary {get {return _Primary;}}

            [Serializable]
            public class Secondary
            {
                [SerializeField, Tooltip("Ordered colliders for the secondary attack.")]
                private Collider2D[] _SecondaryAttackColliders;
                public Collider2D[] secondaryAttackColliders {get {return _SecondaryAttackColliders;}}
                
                [SerializeField]
                private ParticleGenerator _SecondaryAttackParticles;
                public ParticleGenerator secondaryAttackParticles {get {return _SecondaryAttackParticles;}}
            }
            [SerializeField]
            private Secondary _Secondary;
            public Secondary secondary {get {return _Secondary;}}
        }
        [SerializeField]
        private Attack _Attack;
        public Attack attack {get {return _Attack;}}
    }
    [SerializeField]
    private StateParameters _StateParameters;
    public StateParameters stateParameters {get {return _StateParameters;}}

    [Serializable]
    public struct Sounds
    {
        public AudioClip primarySwing;
        public AudioClip secondarySwing;
    }
    [SerializeField]
    private Sounds _Sounds;
    public Sounds sounds {get {return _Sounds;}}

    private Rigidbody2D _Rigidbody2D;
    new public Rigidbody2D rigidbody2D {get {return _Rigidbody2D;}}
    private SwordWielder _Wielder; // This must be set by the wielder calling the BasicSwordMachine.SetWielder method
    public SwordWielder wielder {get {return _Wielder;}}
    private AudioSource _AudioSource;
    public AudioSource audioSource {get {return _AudioSource;}}

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
        _AudioSource = GetComponent<AudioSource>();

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