using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private BaseState _currentState;

    public void Initialize(BaseState state)
    {
        state.OnEnterState();
    }

    public void ChangeState(BaseState state)
    {
        _currentState.OnExitState();
        state.OnEnterState();
    }

    public void FrameUpdate()
    {
        _currentState.OnFrameUpdate();
    }
}
