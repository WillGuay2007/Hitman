using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class RoamState : BaseState
{
    public RoamState(StateMachine stateMachine, BasePersonnage personnage) : base(stateMachine, personnage) { }

    public override void Enter()
    {
        _personnage._animator.SetBool("Roam", true);
        _personnage._navMeshAgent.SetDestination(GetRandomPoint().transform.position);
    }

    public override void Exit()
    {
        _personnage._animator.SetBool("Roam", false);
    }

    public override void Update()
    {
        if (_personnage._navMeshAgent != null)
        {
            if (!_personnage.onRoamUpdate()) return; //Cette fonction va return false si elle change de state. comme ca le reste du code de roam se fait pas executer alors qu'on est dans l'autre state.
            if (!_personnage._navMeshAgent.pathPending && _personnage._navMeshAgent.remainingDistance <= 1)
            {
                if (Random.Range(0,3) == 0) { _stateMachine.ChangeState(_personnage._idleState); return;} //Une chance sur 3 qu'il idle rendu a un point.
                _personnage._navMeshAgent.SetDestination(GetRandomPoint().transform.position);
            }
        }
    }

    private Transform GetRandomPoint()
    {
        List<Transform> Points = _personnage.GetRoamingPoints();
        return Points[Random.Range(0, Points.Count)];
    }
}
