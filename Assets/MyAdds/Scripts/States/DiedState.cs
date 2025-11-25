using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DiedState : BaseState
{
    public DiedState(StateMachine stateMachine, BasePersonnage personnage) : base(stateMachine, personnage) { }

    public override void Enter()
    {
        _personnage.DestroyComponents();
        _personnage.transform.Rotate(-90f, 0f, 0f);
    }

    public override void Exit()
    {
        //Rien ici
    }

    public override void Update()
    {
        //Rien ici
    }
}
