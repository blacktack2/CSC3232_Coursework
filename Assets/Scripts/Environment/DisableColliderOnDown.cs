using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DisableColliderOnDown : MonoBehaviour
{
    private Collider2D _Collider;
    private Collider2D[] _PlayerColliders;

    private bool _IsIgnoring = false;

    void Awake()
    {
        _Collider = GetComponent<Collider2D>();
        Rigidbody2D player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        _PlayerColliders = new Collider2D[player.attachedColliderCount];
        player.GetAttachedColliders(_PlayerColliders);
    }

    void Update()
    {
        bool ignore = Input.GetAxisRaw("Vertical") < 0;
        if (ignore && !_IsIgnoring)
            SetCollision(true);
        else if (!ignore && _IsIgnoring)
            SetCollision(false);
    }

    private void SetCollision(bool ignore)
    {
        _IsIgnoring = ignore;
        foreach (Collider2D c in _PlayerColliders)
        {
            Physics2D.IgnoreCollision(_Collider, c, ignore);
        }
    }
}
