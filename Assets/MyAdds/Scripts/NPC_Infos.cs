using System;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Infos : MonoBehaviour
{
    [SerializeField] private Transform NPCS_Container;
    public List<BasePersonnage> NPCS = new List<BasePersonnage>();

    void Start()
    {
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
    
    public int GetNumberOfGuardGoingToAlarm()
    {
        int number = 0;
        foreach(BasePersonnage NPC in NPCS)
        {
            if (!(NPC is Guard)) continue;
            if (NPC._stateMachine._currentState is GoingForAlarmState) number += 1;
        }
        return number;
    }

    public void ApplyFunctionToEachNPC(Action<BasePersonnage> callback) //Je me suis dit que ca serait pas pire tester les fonctions en parametre.
    {
        foreach (BasePersonnage npc in NPCS) {
            callback(npc);
        }
    }

    public void Update()
    {
        //Oui je sais c'est lourd pour la mémoire mais pas grave.
        List<BasePersonnage> deadNPCs = new List<BasePersonnage>();

        ApplyFunctionToEachNPC(NPC =>
        {
            if (NPC._stateMachine._currentState is DiedState)
            {
                deadNPCs.Add(NPC);
            }
        });

        ApplyFunctionToEachNPC(NPC =>
        {
            if (NPC._stateMachine._currentState is DiedState) return;

            foreach (BasePersonnage dead in deadNPCs)
            {
                float dist = Vector3.Distance(NPC.transform.position, dead.transform.position);
                if (dist < 3) //Je veut qu'ils soient vraiment proche pour qu_ils réalisent qu'il est mort et que c'est pas un homeless qui dort.
                {
                    NPC.OnSeeDeadBody();
                }
            }
        });

    }
}
