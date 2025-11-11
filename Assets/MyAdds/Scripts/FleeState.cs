using UnityEngine;

public class FleeState : BaseState
{
    public FleeState(StateMachine stateMachine, BasePersonnage personnage) : base(stateMachine, personnage) { }

    public override void Enter()
    {
        _personnage._animator.SetBool("Flee", true);
    }

    public override void Exit()
    {
        _personnage._animator.SetBool("Flee", false);
    }

    public override void Update()
    {

    }
}
