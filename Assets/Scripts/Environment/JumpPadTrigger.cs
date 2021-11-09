using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadTrigger : MonoBehaviour
{
    [SerializeField]
    private JumpPad _JumpPad;

    void OnTriggerEnter2D(Collider2D other)
    {
        _JumpPad.TriggerEntered(other);
    }
}
