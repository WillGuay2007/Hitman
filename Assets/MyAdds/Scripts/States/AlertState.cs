using UnityEngine;

public class AlertState : BaseState
{
    public AlertState(StateMachine stateMachine, BasePersonnage personnage) : base(stateMachine, personnage) { }
    private float timer = 0f;
    private float AlertTime = 3f;

    public override void Enter()
    {
        _personnage._audioPlayer.PlaySpottedSound();
        timer = 0f;
        _personnage._navMeshAgent.ResetPath();
        _personnage.Mesh.GetComponent<SkinnedMeshRenderer>().material.color = Color.red;
    }

    public override void Exit()
    {
        _personnage.Mesh.GetComponent<SkinnedMeshRenderer>().material.color = _personnage.MeshColor;
    }

    public override void Update()
    {

        Vector3 direction = _personnage._player.transform.position - _personnage.transform.position;
        direction.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        _personnage.transform.rotation = Quaternion.Lerp(_personnage.transform.rotation, targetRotation, 0.005f); //0.005 parce que je veut qu'il tourne tres lentement.
        //Je trouve qu'un lerp ca fit mieux avec alert state. pas besoin de lerp dans attackstate, tourner instantané c'est mieux je trouve dans attack.

        timer += Time.deltaTime;
        if (timer > AlertTime)
        {
            //Au debut j'avais mis que si le joueur a pas le gun equipped il va pas l'attaquer apres le timer mais j'ai réalisé que cest trop facile sinon.
            if (Vector3.Distance(_personnage.transform.position, _personnage._player.transform.position) <= 12)
            {
                _stateMachine.ChangeState(_personnage._attackState);
                return;
            } else
            {
                _stateMachine.ChangeState(_personnage._roamState);
                return;
            }
        }
    }
}