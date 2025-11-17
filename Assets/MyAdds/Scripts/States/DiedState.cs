using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DiedState : BaseState
{
    public DiedState(StateMachine stateMachine, BasePersonnage personnage) : base(stateMachine, personnage) { }

    public override void Enter() => _personnage.onDiedEnter();

    public override void Exit() => _personnage.onDiedExit();

    public override void Update() => _personnage.onDiedUpdate();
}
