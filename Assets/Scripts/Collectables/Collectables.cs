using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Collectables : MonoBehaviour
{
    [SerializeField] private PlayerStatsData playerStatsData_Model;
    [SerializeField] private int Impact_Health = 0;
    [SerializeField] private int Impact_Score = 0;
    [SerializeField] private int Impact_Level = 0;
    [SerializeField] private int Impact_Money = 0;

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
            
            CollectablesEvents.PlayerCollected.Invoke(this.gameObject);
        }
    }
}
