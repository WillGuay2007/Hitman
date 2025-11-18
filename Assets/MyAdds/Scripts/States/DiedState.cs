using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DiedState : BaseState
{
    public bool CanDie = false;
    public DiedState(StateMachine stateMachine, BasePersonnage personnage) : base(stateMachine, personnage) { }

    public override void Enter()
    {

    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {


        if (CanDie) _personnage.DestroyNPC();

    }
}
