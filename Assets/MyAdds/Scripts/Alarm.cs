using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{

    [SerializeField] private Transform NPCS_Container;
    public List<BasePersonnage> NPCS = new List<BasePersonnage>();
    private AudioPlayer audioPlayer;

    void Start()
    {

        audioPlayer = FindAnyObjectByType<AudioPlayer>();

        if (NPCS_Container != null)
        {
            foreach (Transform NPC in NPCS_Container)
            {
                if (NPC.GetComponent<BasePersonnage>() != null)
                {
                    NPCS.Add(NPC.GetComponent<BasePersonnage>());
                }
            }
        }
    }

    private void ActivateAlarm(BasePersonnage GuardWhoActivated)
    {
        audioPlayer.PlayAlarmSound();
        foreach(BasePersonnage npc in NPCS)
        {
            if (npc._stateMachine._currentState is DiedState 
                || npc._stateMachine._currentState is AttackState
                || npc._stateMachine._currentState is AlertState
                ) return; // Je veut pas alerter des cadavres ou ceux qui savent deja je suis ou
            if (npc is Guard)
            {
                npc._stateMachine.ChangeState(npc._lookAroundState); //Je trouve que cest mieux quil aye dans cette state plutot que se diriger vers le guard vu que la map est petite.
            } else
            {
                npc._stateMachine.ChangeState(npc._fleeState);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        BasePersonnage NPC = other.GetComponent<BasePersonnage>();
        if (NPC != null && NPC._stateMachine._currentState is GoingForAlarmState)
        {
            ActivateAlarm(NPC);
            NPC._stateMachine.ChangeState(NPC._fleeState);
        }
    }

}
