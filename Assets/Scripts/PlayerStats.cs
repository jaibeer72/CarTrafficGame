using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public class PlayerStats 
{
    public int Health = 100;
    public int Score = 0;
    public int Level = 0;
    public int Money = 0; 

    public void Reset()
    {
        Health = 100;
        Score = 0;
        Level = 0;
        Money = 0;
    }
}
