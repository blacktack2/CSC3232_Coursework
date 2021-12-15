using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ParticleGenerator
{
    [SerializeField]
    private GameObject _Particle;
    [SerializeField]
    private bool _SpawnOnTransform;
    [SerializeField]
    private Transform _SpawnTarget;
    [SerializeField]
    private Vector2 _SpawnLocation;

    public GameObject GenerateParticle(Transform parent)
    {
        GameObject particleObject = GenerateParticle();
        particleObject.transform.SetParent(parent);
        return particleObject;
    }

    public GameObject GenerateParticle()
    {
        GameObject particleObject;
        if (_SpawnOnTransform)
            particleObject = GameObject.Instantiate(_Particle, _SpawnTarget.position, Quaternion.identity);
        else
            particleObject = GameObject.Instantiate(_Particle, _SpawnLocation, Quaternion.identity);
        particleObject.GetComponent<ParticleSystem>().Play();
        return particleObject;
    }
}
