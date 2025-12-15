using UnityEngine;

//NOTES: L'appel du changement d'état se passe dans la state elle-même.
//Quand on change de state, on appelle premièrement Exit() puis ensuite Enter() sur la nouvelle state.
//Par exemple: Le exit d'une state flee pourrait etre d'arreter l'animation de fuite puis de remettre le speed a son walk speed.

public class StateMachine
{
    public BaseState _currentState; //public car je veut la voir dans le basepersonnage
    //Je sais que privé est une meilleure pratique mais je vise pas la perfection non plus, c'est un projet d'école.

    public void ChangeState(BaseState newState)
    {
        if (newState == _currentState) return;
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

