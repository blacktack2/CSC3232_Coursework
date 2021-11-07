using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private CharacterMachine _Machine;

    void Update()
    {
        int horizontal = (int) Input.GetAxisRaw("Horizontal");
        _Machine.SetDirection(horizontal);

        _Machine.SetRunButtonDown(Input.GetButton("Run"));
        _Machine.SetCrouchButtonDown(Input.GetButton("Crouch"));
        if (Input.GetButton("Jump"))
            _Machine.DoJump();
    }
}
