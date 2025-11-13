using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(StateMachine stateMachine, BasePersonnage personnage) : base(stateMachine, personnage){}
    private float IdleTimer;
    private float TimeIdled;

    public override void Enter()
    {
        Debug.Log("Entered idle state");
        TimeIdled = 0f;
        IdleTimer = UnityEngine.Random.Range(1, 4);
    }

    public override void Exit()
    {

    }

    public override void Update()
    {   
        TimeIdled += Time.deltaTime;
        if (TimeIdled > IdleTimer) {
            _stateMachine.ChangeState(new RoamState(_stateMachine, _personnage));
        }
    }
}
