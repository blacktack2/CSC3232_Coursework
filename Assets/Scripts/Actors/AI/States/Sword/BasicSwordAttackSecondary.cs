using UnityEngine;

public class BasicSwordAttackSecondary : BasicSwordAttack
{
    public BasicSwordAttackSecondary(BasicSwordMachine stateMachine) : base("AttackSecondary", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _SM.animator.SetTrigger("EnterSecondaryAttackState");
        _SM.audioSource.PlayOneShot(_SM.sounds.secondarySwing);
        ParticleSystem particles = _SecondaryParameters.secondaryAttackParticles.GenerateParticle().GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule shape = particles.shape;
        shape.rotation = shape.rotation * _SM.transform.localScale.x;
        if (_SM.transform.localScale.x < 0)
            particles.GetComponent<ArcMotion>().Flip();
    }

    protected override Collider2D[] GetAttackColliders()
    {
        return _SecondaryParameters.secondaryAttackColliders;
    }
}
