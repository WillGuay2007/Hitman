using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(StateMachine stateMachine, BasePersonnage personnage) : base(stateMachine, personnage){}

    public override void Enter() => _personnage.onIdleEnter();

    public override void Exit() => _personnage.onIdleExit();

    public override void Update() => _personnage.onIdleUpdate();
}
