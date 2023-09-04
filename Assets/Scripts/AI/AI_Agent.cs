using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Agent : MonoBehaviour
{
    [SerializeField]
    public PlayerStatsData playerStatsData_Model;
    [SerializeField] private int Impact_Health = 25;
    [SerializeField] private int Impact_Score = 0;
    [SerializeField] private int Impact_Level = 0;
    [SerializeField] private int Impact_Money = 0;


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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player has collected the collectable");
            playerStatsData_Model.stats.Health += Impact_Health;
            playerStatsData_Model.stats.Score += Impact_Score;
            playerStatsData_Model.stats.Level += Impact_Level;
            playerStatsData_Model.stats.Money += Impact_Money;
            playerStatsData_Model.Notify(playerStatsData_Model.stats);

            AIEvents.DespwnAI.Invoke(gameObject);
        }
    }
}
