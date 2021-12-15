using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ArcMotion : MonoBehaviour
{
    [SerializeField]
    private float _MotionPeriod;

    [SerializeField]
    private Vector3 _EndPoint;
    [SerializeField]
    private Vector3 _StartHandle;
    [SerializeField]
    private Vector3 _EndHandle;

    private Vector3 _StartPosition;
    public Vector3 startPoint {get {return _StartPosition;}}
    public Vector3 endPoint {get {return startPoint + _EndPoint;}}
    public Vector3 startHandle {get {return startPoint + _StartHandle;}}
    public Vector3 endHandle {get {return startPoint + _EndHandle;}}

    private float _TimeSinceStart = 0.0f;

    void Awake()
    {
        _StartPosition = transform.position;
    }

    void Update()
    {
        _TimeSinceStart += Time.deltaTime;
        if (_TimeSinceStart > _MotionPeriod)
        {
            transform.position = BezierLerp(startPoint, startHandle, endHandle, endPoint, 1);
            Destroy(this);
            return;
        }
        transform.position = BezierLerp(startPoint, startHandle, endHandle, endPoint, _TimeSinceStart / _MotionPeriod);
    }

    private Vector3 BezierLerp(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1 - t;
        float uu = u * u;
        float uuu = uu * u;
        float tt = t * t;
        float ttt = tt * t;

        Vector3 p = uuu * p0;

        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;
        return p;
    }

    public void Flip()
    {
        _EndPoint.x *= -1;
        _StartHandle.x *= -1;
        _EndHandle.x *= -1;
    }
}
