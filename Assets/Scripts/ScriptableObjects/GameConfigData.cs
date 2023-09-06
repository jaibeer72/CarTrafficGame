using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameConfigData 
{
    public float GameTimer = 60.0f;
    public int MaxEnemiesOnBoard = 3; 
    public int MaxCollectablesOnBoard = 3;

    public void SetToDefault()
    {
        GameTimer = 60.0f;
        MaxEnemiesOnBoard = 3;
        MaxCollectablesOnBoard = 3;
    }
}
