using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIObjectPool : MonoBehaviour
{
    GameObject[] aiObjects;
    public int aiObjectCount = 10;
    public GameObject aiObjectPrefab;
    public Transform[] spwanPositions;
    public int MaxOnBoard = 3;
    private int _CurrentOnBoard; 
    private Dictionary<GameObject,bool> IsAiAliveDictionary = new Dictionary<GameObject, bool>();
    // Start is called before the first frame update
    void Start()
    {
        aiObjects = new GameObject[aiObjectCount];
        for (int i = 0; i < aiObjectCount; i++)
        {
            aiObjects[i] = Instantiate(aiObjectPrefab, transform);
            aiObjects[i].SetActive(false);
            IsAiAliveDictionary.Add(aiObjects[i], false);
        }
    }

}
