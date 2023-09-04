using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        BoardActionsEvents.WayPointChangeEvent.AddListener(OnWayPointChange);
    }
    private void OnDestroy()
    {
        BoardActionsEvents.WayPointChangeEvent.RemoveListener(OnWayPointChange);
    }
    private void OnWayPointChange(Vector3 arg0)
    {
        agent.SetDestination(arg0);
    }


}
