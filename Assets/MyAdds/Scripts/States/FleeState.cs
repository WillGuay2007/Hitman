using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.TextCore;

public class FleeState : BaseState
{
    public FleeState(StateMachine stateMachine, BasePersonnage personnage) : base(stateMachine, personnage) { }

    public override void Enter() => _personnage.onFleeEnter();

    public override void Exit()
    {
        _personnage._animator.SetBool("Flee", false);
        _personnage._navMeshAgent.speed -= 5;
    }

    public override void Update() => _personnage.onFleeUpdate();
}
