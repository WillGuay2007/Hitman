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

        if (_timeIdled > _idleTimer) _stateMachine.ChangeState(_roamState);
    }

    public override void onIdleExit()
    {
        _navMeshAgent.isStopped = false;
    }

    public override void onCriticalHealth()
    {
        _stateMachine.ChangeState(_fleeState);
    }

    public override void onRoamEnter()
    {
        _animator.SetBool("Roam", true);
        _navMeshAgent.SetDestination(GetRandomPoint().transform.position);
    }

    public override void onRoamUpdate()
    {

        if (_health <= 30 && Vector3.Distance(transform.position, _player.transform.position) <= 7) {
            _stateMachine.ChangeState(_fleeState); //Le guard est badass mais pas autant que ca. Il va fuire si il est en critical health tu es trop proche.
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
                Debug.Log("NPC is far enough to stop fleeing");
                _stateMachine.ChangeState(_roamState);
                return;
            }
            _navMeshAgent.SetDestination(GetFurthestPointFromPlayer().position);
        }
    }

    public override void onTakeDamage()
    {
        CanRegognizePlayer = true; //Si jamais il reconnaissait pas deja la menace.
    }

}
