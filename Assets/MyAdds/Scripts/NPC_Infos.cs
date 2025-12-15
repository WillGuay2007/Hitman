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
}
