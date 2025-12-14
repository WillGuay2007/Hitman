using UnityEngine;

public class AttackState : BaseState
{
    public AttackState(StateMachine stateMachine, BasePersonnage personnage) : base(stateMachine, personnage) { }

    public override void Enter()
    {
        _personnage._audioPlayer.PlaySpottedAttackSound();
    }

    public override void Exit()
    {

    }

    public override void Update()
    {

    }
}