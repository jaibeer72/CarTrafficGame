using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField]
    public PlayerStatsData playerStatsData_Model;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(playerStatsData_Model != null, "PlayerStatsData_Model is not assigned in the editor.");

        agent = GetComponent<NavMeshAgent>();
        // reset data when game starts for later. 
        playerStatsData_Model.ResetDataForObservable(); 
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
