using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(StateMachine stateMachine, BasePersonnage personnage) : base(stateMachine, personnage){}

    public override void Enter()
    {
        _personnage._animator.SetBool("Idle", true);
    }

    public override void Exit()
    {
        _personnage._animator.SetBool("Idle", false);
    }

    public override void Update()
    {
       
    }
}
