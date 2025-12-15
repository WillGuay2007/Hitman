using UnityEngine;

//Faire que un garde peut activer l'alarme plus que une fois sur takedamage est volontaire. Je veut pas qu'il flee.

public class GoingForAlarmState : BaseState
{
    private float maxAttemptTime = 3.5f; //Ya un bug ou si je tire le guard quand il est deja dans le trigger, le ontriggerenter va pas fire alors je met cela
    private float timer = 0f;
    public GoingForAlarmState(StateMachine stateMachine, BasePersonnage personnage) : base(stateMachine, personnage) { }

    public override void Enter()
    {
        timer = 0f;
        _personnage._navMeshAgent.ResetPath();
        _personnage._animator.SetBool("Flee", true);
        _personnage._navMeshAgent.speed += 5;
        _personnage._navMeshAgent.SetDestination(_personnage.GetClosestAlarm().position);
    }

    public override void Exit()
    {
        _personnage._navMeshAgent.speed -= 5;
        _personnage._animator.SetBool("Flee", false);
        _personnage._navMeshAgent.ResetPath();
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer > maxAttemptTime) _personnage._stateMachine.ChangeState(_personnage._fleeState);
        if (!_personnage._navMeshAgent.hasPath)
        {
            _personnage._navMeshAgent.SetDestination(_personnage.GetClosestAlarm().position);
        }
    }
}
