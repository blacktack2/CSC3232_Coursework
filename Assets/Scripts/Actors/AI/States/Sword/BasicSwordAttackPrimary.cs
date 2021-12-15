using UnityEngine;

public class BasicSwordAttackPrimary : BasicSwordAttack
{
    public BasicSwordAttackPrimary(BasicSwordMachine stateMachine) : base("AttackPrimary", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _SM.animator.SetTrigger("EnterPrimaryAttackState");
        _SM.audioSource.PlayOneShot(_SM.sounds.primarySwing);
        ParticleSystem particles = _PrimaryParameters.primaryAttackParticles.GenerateParticle().GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule shape = particles.shape;
        shape.rotation = shape.rotation * _SM.transform.localScale.x;
        if (_SM.transform.localScale.x < 0)
            particles.GetComponent<ArcMotion>().Flip();
    }

    protected override Collider2D[] GetAttackColliders()
    {
        return _PrimaryParameters.primaryAttackColliders;
    }
}
