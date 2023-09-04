using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Agent : MonoBehaviour
{
    public NavMeshAgent agent;
    private Queue<Vector3> _path = new Queue<Vector3>();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public void SetDestination(Vector3 destination , Vector3 via)
    {
        _path.Enqueue(destination);
        agent.SetDestination(via);
    }
    public void SetStartPoint(Vector3 startPoint)
    {
        agent.Warp(startPoint);
    }
    public bool IsAtDestination()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if(_path.Count > 0)
            {
                agent.SetDestination(_path.Dequeue());
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }
}
