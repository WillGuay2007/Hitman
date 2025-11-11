using UnityEngine;

//NOTES: L'appel du changement d'état se passe dans la state elle-même.
//Quand on change de state, on appelle premièrement Exit() puis ensuite Enter() sur la nouvelle state.
//Par exemple: Le exit d'une state flee pourrait etre d'arreter l'animation de fuite puis de remettre le speed a son walk speed.

public class StateMachine
{
    private BaseState _currentState;

    public void ChangeState(BaseState newState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = newState;
        _currentState.Enter(); 
    }

    public void Update() //Ca va etre appelé a chaque frame dans le MonoBehaviour qu'utilise la state machine.
    {
        if (_currentState != null)
        {
            _currentState.Update();
        }
    }
}

