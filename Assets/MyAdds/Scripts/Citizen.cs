using UnityEngine;

public class Citizen : BasePersonnage // C'est aussi un IPersonnage puisque BasePersonnage l'implémente
{

    private float _idleTimer;
    private float _timeIdled;

    public override void onIdleEnter()
    {
        _timeIdled = 0f;
        _idleTimer = Random.Range(2f, 5f);
        _navMeshAgent.isStopped = true;
    }

    public override void onIdleUpdate()
    {
        _timeIdled += Time.deltaTime;

        if (_timeIdled > _idleTimer) _stateMachine.ChangeState(_roamState);
    }

    public override void onIdleExit()
    {
        _navMeshAgent.isStopped = false;
    }

    public override void onRoamEnter()
    {
        _animator.SetBool("Roam", true);
        //Le citizen va chercher a eviter le player si il reconnait la menace.
        if (CanRegognizePlayer)
        {
            _navMeshAgent.SetDestination(GetRandomPointOutOfPlayerRadius(10f).transform.position);
        }
        else
        {
            _navMeshAgent.SetDestination(GetRandomPoint().transform.position);
        }
    }

    public override void onRoamUpdate()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) <= 5 && _playerControls.HasGunEquipped)
        {
            _stateMachine.ChangeState(_fleeState);
            return;
        }
        if (_navMeshAgent != null)
        {
            if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= 1)
            {
                if (Random.Range(0, 5) == 0) { _stateMachine.ChangeState(_idleState); return; } //Une chance sur 5 qu'il idle rendu a un point.
                _navMeshAgent.SetDestination(GetRandomPoint().transform.position);
            }
        }
    }

    public override void onFleeEnter()
    {
        CanRegognizePlayer = true;
        _animator.SetBool("Flee", true);
        _navMeshAgent.speed += 5;
        _navMeshAgent.SetDestination(GetFurthestPointFromPlayer().position);
    }

    public override void onFleeUpdate()
    {
        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= 0.1f)
        {
            if (Vector3.Distance(transform.position, _player.transform.position) >= 10)
            {
                Debug.Log("NPC is far enough to stop fleeing");
                _stateMachine.ChangeState(_roamState);
                return;
            }
            _navMeshAgent.SetDestination(GetFurthestPointFromPlayer().position);
        }
    }

    public override void onCriticalHealth()
    {
        _stateMachine.ChangeState(_fleeState);
    }

    public override void onTakeDamage()
    {
        _stateMachine.ChangeState(_fleeState);
    }

}
