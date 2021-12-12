using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class VignetteHandler : MonoBehaviour
{
    [SerializeField]
    private Q_Vignette_Single _Vignette;

    [Serializable]
    public struct VignetteProperties
    {
        public Color color;
        public float scale;
    }

    [SerializeField]
    private VignetteProperties _VignetteDefaultProperties;
    [SerializeField]
    private VignetteProperties _VignetteEnemyCloseProperties;

    [SerializeField]
    private float _VignetteEnemyChangeRadius = 10.0f;

    void Awake()
    {
        _Vignette.mainColor = _VignetteDefaultProperties.color;
        _Vignette.mainScale = _VignetteDefaultProperties.scale;
    }

    void Update()
    {
        float distance = 0.0f;
        Collider2D overlap = Physics2D.OverlapCircle(transform.position, _VignetteEnemyChangeRadius, LayerMask.GetMask("Enemy"));
        if (overlap != null && overlap.attachedRigidbody != null &&  overlap.attachedRigidbody.CompareTag("Enemy"))
            distance = 1.0f - (Vector2.Distance(transform.position, overlap.transform.position) / _VignetteEnemyChangeRadius);
        SetVignetteThreshold(distance);
    }

    void OnValidate()
    {
        Update();
    }

    public void SetVignetteThreshold(float threshold)
    {
        _Vignette.mainColor = Color.Lerp(_VignetteDefaultProperties.color, _VignetteEnemyCloseProperties.color, threshold);
        _Vignette.mainScale = Mathf.Lerp(_VignetteDefaultProperties.scale, _VignetteEnemyCloseProperties.scale, threshold);
    }
}
