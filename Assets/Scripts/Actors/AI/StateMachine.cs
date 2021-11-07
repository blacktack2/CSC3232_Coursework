using UnityEngine;

// Code sourced from: https://medium.com/c-sharp-progarmming/make-a-basic-fsm-in-unity-c-f7d9db965134
public abstract class StateMachine : MonoBehaviour
{
    private BaseState _CurrentState;
    protected BaseState currentState {get {return _CurrentState;}}

    void Start()
    {
        _CurrentState = GetInitialState();
        if (_CurrentState != null)
            _CurrentState.Enter();
    }

    void Update()
    {
        if (_CurrentState != null)
            _CurrentState.UpdateLogic();
    }

    void LateUpdate()
    {
        if (_CurrentState != null)
            _CurrentState.UpdatePhysics();
    }

    public void ChangeState(BaseState newState)
    {
        _CurrentState.Exit();

        _CurrentState = newState;
        _CurrentState.Enter();
    }

    protected abstract BaseState GetInitialState();
}