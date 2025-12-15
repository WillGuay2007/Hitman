using UnityEngine;

public class Guard : BasePersonnage // C'est aussi un IPersonnage puisque BasePersonnage l'implémente
{

    private float _idleTimer;
    private float _timeIdled;

    public override void onIdleEnter()
    {
        _timeIdled = 0f;
        _idleTimer = Random.Range(5f, 10f);
        _navMeshAgent.isStopped = true;
    }

    public override void onIdleUpdate()
    {
        _timeIdled += Time.deltaTime;

        transform.Rotate(0, 20f * Time.deltaTime, 0);

        if (Vector3.Distance(transform.position, _player.transform.position) <= 5 && _playerControls.HasGunEquipped)
        {
            _stateMachine.ChangeState(_alertState);
            return;
        }

        if (_timeIdled > _idleTimer) _stateMachine.ChangeState(_roamState);
    }

    public override void onIdleExit()
    {
        _navMeshAgent.isStopped = false;
    }

    public override void onCriticalHealth()
    {
        if (_stateMachine._currentState is GoingForAlarmState) return; //Pour eviter les bugs
        if (_npcs_Infos.GetNumberOfGuardGoingToAlarm() == 0)
        {
            _stateMachine.ChangeState(_goingForAlarmState);
        } else
        {
            _stateMachine.ChangeState(_fleeState);
        }
    }

    public override void onRoamEnter()
    {
        _animator.SetBool("Roam", true);
        _navMeshAgent.SetDestination(GetRandomPoint().transform.position);
    }

    public override void onRoamUpdate()
    {

        if (Vector3.Distance(transform.position, _player.transform.position) <= 5 && _playerControls.HasGunEquipped)
        {
            _stateMachine.ChangeState(_alertState);
            return;
        }

        if (_navMeshAgent != null)
        {
            if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= 1)
            {
                if (Random.Range(0, 2) == 0) { _stateMachine.ChangeState(_idleState); return; } //Une chance sur 2 qu'il idle rendu a un point.
                _navMeshAgent.SetDestination(GetRandomPoint().transform.position);
            }
        }
    }


    public override void onFleeEnter()
    {
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
                _stateMachine.ChangeState(_roamState);
                return;
            }
            _navMeshAgent.SetDestination(GetFurthestPointFromPlayer().position);
        }
    }

    public override void OnSeeDeadBody() //Appelé a partir de NPC_Infos
    {
        if (_stateMachine._currentState is DiedState ||
            _stateMachine._currentState is AttackState ||
            _stateMachine._currentState is LookAroundState ||
            _stateMachine._currentState is AlertState || //Cette ligne est vraiment importante sinon un bug va peter tes oreilles.
            _stateMachine._currentState is GoingForAlarmState
            ) return;
        _stateMachine.ChangeState(_lookAroundState);
    }

    public override void onTakeDamage()
    {
        CanRegognizePlayer = true; //Si jamais il reconnaissait pas deja la menace. Juste au cas ou.
        _stateMachine.ChangeState(_attackState);
    }

}
