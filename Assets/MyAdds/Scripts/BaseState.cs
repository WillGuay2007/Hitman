using UnityEngine;

public abstract class BaseState
{
    protected Personnage _Personnage;

    public BaseState(Personnage personnage)
    {
        _Personnage = personnage;
    }

    public abstract void OnEnterState();
    public abstract void OnFrameUpdate();
    public abstract void OnExitState();

}
