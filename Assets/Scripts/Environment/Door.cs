using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private float _OpenSpeed = 1.0f;
    [SerializeField]
    private bool _IsOpen = false;
    private bool _DoOpen = false;
    private bool _DoClose = false;

    private Vector3 _OpenPos;
    private Vector3 _ClosePos;

    private float _OpenTime = 0.0f;

    void Awake()
    {
        _ClosePos = transform.position;
        _OpenPos = _ClosePos + new Vector3(0, 2, 0);
        if (_IsOpen)
        {
            transform.position = _ClosePos;
        }
    }

    void FixedUpdate()
    {
        if (_IsOpen && _DoClose)
        {
            _OpenTime = Mathf.Clamp01(_OpenTime + Time.fixedDeltaTime * _OpenSpeed);
            transform.position = Vector3.Lerp(_OpenPos, _ClosePos, _OpenTime);
            if (_OpenTime == 1)
                _IsOpen = false;
        }
        else if (!_IsOpen && _DoOpen)
        {
            _OpenTime = Mathf.Clamp01(_OpenTime + Time.fixedDeltaTime * _OpenSpeed);
            transform.position = Vector3.Lerp(_ClosePos, _OpenPos, _OpenTime);
            if (_OpenTime == 1)
                _IsOpen = true;
        }
    }

    public void Open()
    {
        if (!_IsOpen)
        {
            _DoOpen = true;
            _OpenTime = 0.0f;
        }
    }

    public void Close()
    {
        if (_IsOpen)
        {
            _DoClose = true;
            _OpenTime = 0.0f;
        }
    }
}
