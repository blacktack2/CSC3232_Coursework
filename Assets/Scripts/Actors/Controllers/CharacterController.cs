using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField, Tooltip("Character to send input signals to.")]
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
