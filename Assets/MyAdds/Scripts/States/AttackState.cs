using UnityEngine;

public class AttackState : BaseState
{

    //J'ai pas trouvé de moyen de faire une animation de tir alors j'ai juste rien mis. Imagine qu'il tire.
    //Pour la logique de tir, je me suis pas compliqué la vie avec un raycast, j'ai juste fait que a chaque fois quil tir, soit il l'a soit il le manque.

    public AttackState(StateMachine stateMachine, BasePersonnage personnage) : base(stateMachine, personnage) { }
    private float shootTimer;
    private float shootDelay = 3f;
    private float gunDamage = 10f; //5 c'étais trop facile
    private float allowExitTimer = 0f; //Sans ca, le guard arrete de te chasser instantanément si tu le "snipe" de loin

    public override void Enter()
    {
        shootTimer = 0;
        allowExitTimer = 0f;
        _personnage._animator.SetBool("Flee", true);
        _personnage._navMeshAgent.speed += 4;
        _personnage._navMeshAgent.ResetPath();
        _personnage._audioPlayer.PlaySpottedAttackSound();
        _personnage._navMeshAgent.SetDestination(_personnage._player.transform.position + _personnage.getRandomPolarCoordinate(5f, _personnage._player.transform.position));
    }

    public override void Exit()
    {
        _personnage._animator.SetBool("Flee", false);
        _personnage._navMeshAgent.speed -= 4;
    }

    public override void Update()
    {
        allowExitTimer += Time.deltaTime;
        if (allowExitTimer > 3 && Vector3.Distance(_personnage.transform.position, _personnage._player.transform.position) > 15)
        {
            _stateMachine.ChangeState(_personnage._roamState);
        }
        shootTimer += Time.deltaTime;
        _personnage.transform.LookAt(_personnage._player.transform, Vector3.up);
        if (shootTimer >= shootDelay)
        {
            shootTimer = 0;
            Shoot();
        } else if (shootTimer > shootDelay/2)
        {
            if (_personnage._navMeshAgent.hasPath) _personnage._navMeshAgent.ResetPath();
            _personnage._animator.SetBool("Flee", false);
            //Je veut juste pas toujours qu'ils courent
        } else if (shootTimer <= shootDelay / 2)
        { 
            if (!_personnage._navMeshAgent.hasPath)
            {
                _personnage._animator.SetBool("Flee", true);
                _personnage._navMeshAgent.SetDestination(_personnage._player.transform.position + _personnage.getRandomPolarCoordinate(5f, _personnage.transform.position));
            }
        }
    }

    private void Shoot() {
        _personnage._audioPlayer.PlayGuardShootSound();
        if (!(Random.Range(0,3) == 1)) //2 chance sur 3 de land la balle.
        {
            _personnage._playerControls.TakeDamage(gunDamage);
            _personnage._audioPlayer.PlayBulletHitSound();
        }
    }

}