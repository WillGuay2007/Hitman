using UnityEngine;

//NOTE: Seul les gardes vont sonner l'alarme, les civils vont fuire en voyant une menace.

public class GoingForAlarmState : BaseState
{
    public GoingForAlarmState(StateMachine stateMachine, BasePersonnage personnage) : base(stateMachine, personnage) { }

    public override void Enter()
    {
        _personnage._navMeshAgent.ResetPath();
        _personnage._animator.SetBool("Flee", true);
        _personnage._navMeshAgent.speed += 5;
        _personnage._navMeshAgent.SetDestination(_personnage.GetClosestAlarm().position);
    }

    public override void Exit()
    {
        _personnage._navMeshAgent.speed -= 5;
        _personnage._animator.SetBool("Flee", false);
    }

    public override void Update()
    {
        //Je gère le exit dans le triggerEnter de l'alarme, donc pas de logique ici.
    }
}
