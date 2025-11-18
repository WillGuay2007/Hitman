using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class RoamState : BaseState
{
    public RoamState(StateMachine stateMachine, BasePersonnage personnage) : base(stateMachine, personnage) { }

    public override void Enter() => _personnage.onRoamEnter();

    public override void Exit()
    {
        _personnage._animator.SetBool("Roam", false);
    }

    public override void Update() => _personnage.onRoamUpdate();
}
