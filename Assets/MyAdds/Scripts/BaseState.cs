using UnityEngine;

//NOTES: La BaseState est le blueprint pour créer n'importe quelle state par héritage.
//Cette classe abstraite permet de savoir exactement ce qu'une state doit contenir obligatoirement.
public abstract class BaseState
{
    protected StateMachine _stateMachine;
    protected BasePersonnage _personnage;

    public BaseState(StateMachine stateMachine, BasePersonnage personnage)
    {
        _stateMachine = stateMachine;
        _personnage = personnage;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}
