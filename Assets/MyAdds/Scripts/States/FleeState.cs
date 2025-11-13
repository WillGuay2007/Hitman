using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.TextCore;

public class FleeState : BaseState
{
    public FleeState(StateMachine stateMachine, BasePersonnage personnage) : base(stateMachine, personnage) { }

    public override void Enter()
    {
        _personnage._animator.SetBool("Flee", true);
        _personnage._navMeshAgent.speed += 5;
        _personnage._navMeshAgent.isStopped = false;
    }

    public override void Exit()
    {
        _personnage._animator.SetBool("Flee", false);
        _personnage._navMeshAgent.speed -= 5;
        _personnage._navMeshAgent.isStopped = true;
    }

    public override void Update()
    {
        if(!_personnage._navMeshAgent.pathPending && _personnage._navMeshAgent.remainingDistance <= 1)
        {
            if (Vector3.Distance(_personnage.transform.position, _personnage._player.transform.position) >= 10)
            {
                Debug.Log("NPC is far enough to stop fleeing");
                _stateMachine.ChangeState(new IdleState(_stateMachine, _personnage));
            }
            _personnage._navMeshAgent.SetDestination(GetFurthestPointFromPlayer().position);
        }
    }

    private Transform GetFurthestPointFromPlayer()
    {
        List<Transform> Points = _personnage.GetRoamingPoints();
        Vector3 PlayerPosition = _personnage._player.transform.position;
        Transform FurthestPoint = null;
        float FurthestDistance = 0f;

        foreach (Transform t in Points) {
            float Distance = Vector3.Distance(t.position, PlayerPosition);
            if (Distance > FurthestDistance)
            {
                FurthestDistance = Distance;
                FurthestPoint = t;
            }
        }
        return FurthestPoint;
    }
}
