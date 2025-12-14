using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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
        timer += Time.deltaTime;
        if (timer > AlertTime)
        {
            if (Vector3.Distance(_personnage.transform.position, _personnage._player.transform.position) <= 5 && _personnage._playerControls.HasGunEquipped)
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