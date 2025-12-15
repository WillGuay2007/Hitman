using UnityEngine;

public class LookAroundState : BaseState
{
    private float SearchTimer = 0f;
    private float TotalSearchTime = 10f;
    public LookAroundState(StateMachine stateMachine, BasePersonnage personnage) : base(stateMachine, personnage) { }

    public override void Enter()
    {
        SearchTimer = 0f;
        _personnage._animator.SetBool("Roam", true);
        _personnage._navMeshAgent.speed += 7;
    }

    public override void Exit()
    {
        _personnage._animator.SetBool("Roam", false);
        _personnage._navMeshAgent.speed -= 7;
    }

    public override void Update()
    {
        SearchTimer += Time.deltaTime;
        if (SearchTimer > TotalSearchTime) {
            _stateMachine.ChangeState(_personnage._idleState);
            return;
        }
        if (Vector3.Distance(_personnage.transform.position, _personnage._player.transform.position) <= 12 && _personnage._playerControls.HasGunEquipped)
        {
            _stateMachine.ChangeState(_personnage._alertState);
            return; //Quand il sont en lookaroundstate, les guards vont patrouiller avec une plus grande vitesse et champ de vision
        }

        if (_personnage._navMeshAgent != null)
        {
            if (!_personnage._navMeshAgent.pathPending && _personnage._navMeshAgent.remainingDistance <= 1)
            {
                _personnage._navMeshAgent.SetDestination(_personnage.GetRandomPoint().transform.position);
            }
        }
    }
}
