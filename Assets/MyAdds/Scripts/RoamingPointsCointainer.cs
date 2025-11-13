using System.Collections.Generic;
using UnityEngine;

public class RoamingPointsCointainer : MonoBehaviour
{
    public List<Transform> RoamingPoints = new List<Transform>();

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            RoamingPoints.Add(child);
        }
    }
}
