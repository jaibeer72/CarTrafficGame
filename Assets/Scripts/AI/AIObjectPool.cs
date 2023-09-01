using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIObjectPool : MonoBehaviour
{
    // what this class should do 
    // 1. spawn AI objects
    // assign them a spwn position position from one of the spawn positions 
    // 2. despawn AI objects
    // 3. keep track of how many AI objects are on the board
    // 4. keep track of how many AI objects are alive
    // 5. assign the AI end position based on the spwn location
    // 
    GameObject[] aiObjects;
    public int aiObjectCount = 10;
    public GameObject aiObjectPrefab;
    public Transform spawnPostionsParent; 
    private Transform[] spawnPostions;
    public int MaxOnBoard = 3;
    private int _CurrentOnBoard; 
    private Dictionary<GameObject,bool> IsAiAliveDictionary = new Dictionary<GameObject, bool>();
    // Start is called before the first frame update
    void Start()
    {
        _CurrentOnBoard = 0;
        spawnPostions = spawnPostionsParent.GetComponentsInChildren<Transform>();
        aiObjects = new GameObject[aiObjectCount];
        for (int i = 0; i < aiObjectCount; i++)
        {
            aiObjects[i] = Instantiate(aiObjectPrefab, transform);
            aiObjects[i].SetActive(false);
            IsAiAliveDictionary.Add(aiObjects[i], false);
        }
    }

}
