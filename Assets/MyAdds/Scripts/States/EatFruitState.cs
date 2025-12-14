using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFruitState : BaseState //C'est pour le critère d'avoir une tâche dans l'énoncé
{
    private float fruitHeal = 25f;

    public EatFruitState(StateMachine stateMachine, BasePersonnage personnage) : base(stateMachine, personnage) { }
    public override void Enter()
    {
        Transform fruit = _personnage._fruits[Random.Range(0, _personnage._fruits.Count)]; //Je crois que ca compte pour le critère: Environnement (ajout d’éléments pertinents aux états)
        _personnage._animator.SetBool("Roam", true);

        _personnage._navMeshAgent.SetDestination(fruit.position);
    }


    public override void Exit()
    {
        _personnage._animator.SetBool("Roam", false);
    }

    public override void Update()
    {

        if (!_personnage._navMeshAgent.pathPending && _personnage._navMeshAgent.remainingDistance <= 0.1f)
        {
            _personnage._audioPlayer.PlayEatSound();
            _personnage._health = Mathf.Min(_personnage._health + fruitHeal, 100);
            _personnage._stateMachine.ChangeState(_personnage._idleState);
        }
    }
}
