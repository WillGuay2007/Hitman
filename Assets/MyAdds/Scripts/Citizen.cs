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
        _navMeshAgent.isStopped = true;
    }
}
