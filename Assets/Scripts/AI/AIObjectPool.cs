using System;
using System.Linq;
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

    [SerializeField]
    private GameConfig gameConfig;

    GameObject[] aiObjects;
    public int aiObjectCount = 10;
    public GameObject aiObjectPrefab;
    public Transform spawnPostionsParent;
    private Transform[] spawnPostions;
    private int m_MaxEnemiesOnBoard = 3;
    private int _CurrentOnBoard;
    private Dictionary<GameObject, bool> IsAiAliveDictionary = new Dictionary<GameObject, bool>();
    private bool _StopSpwanEnumirator = false;

    private IEnumerator _coroutine;
    // Start is called before the first frame update

    void Awake()
    {
        _CurrentOnBoard = 0;
        m_MaxEnemiesOnBoard = gameConfig.configData.MaxEnemiesOnBoard;
        // onlyy if the spawn positions iis not equal to partent position 
        // then get the spawn positions

        spawnPostions = spawnPostionsParent.GetComponentsInChildren<Transform>().Where(t => t != spawnPostionsParent.transform).ToArray();

        aiObjects = new GameObject[aiObjectCount];
        for (int i = 0; i < aiObjectCount; i++)
        {
            aiObjects[i] = Instantiate(aiObjectPrefab, transform) as GameObject;
            aiObjects[i].SetActive(false);
            IsAiAliveDictionary.Add(aiObjects[i], false);
        }
        AIEvents.DespwnAI.AddListener(OnDespwnAI);
    }

    private void OnDespwnAI(GameObject arg0)
    {
        // Find the AI object in the dictionary and set it to false
        // then set the game object to false
        DisableAI(arg0);
    }

    private void DisableAI(GameObject arg0)
    {
        if (IsAiAliveDictionary.ContainsKey(arg0))
        {
            IsAiAliveDictionary[arg0] = false;
            arg0.SetActive(false);
            _CurrentOnBoard--;
        }
    }

    private void Start()
    {
        _coroutine = SpawnAIObject();
        StartCoroutine(_coroutine);
        AIEvents.StopSpwanEnumirator.AddListener(OnStopSpwanEnumirator);
    }

    private void OnStopSpwanEnumirator(bool arg0)
    {
        _StopSpwanEnumirator = arg0;
        if (arg0)
            StopCoroutine(_coroutine); 
    }

    private void OnDestroy()
    {
        _StopSpwanEnumirator = true;
        StopCoroutine(_coroutine);
        AIEvents.StopSpwanEnumirator.RemoveListener(OnStopSpwanEnumirator);
    }
    private IEnumerator SpawnAIObject()
    {
        // wait for 3 seconds   
        yield return new WaitForSeconds(3);
        while (!_StopSpwanEnumirator)
        {
            Shuffle(spawnPostions);
            if (_CurrentOnBoard > m_MaxEnemiesOnBoard)
                yield return null;

            for (int i = 0; i < aiObjectCount; i++)
            {
                var aiAgent = aiObjects[i].GetComponent<AI_Agent>();
                if(_CurrentOnBoard < m_MaxEnemiesOnBoard)
                {
                    if (!IsAiAliveDictionary[aiObjects[i]])
                    {
                        aiAgent.SetStartPoint(spawnPostions[UnityEngine.Random.Range(0, spawnPostions.Length)].position);
                        // look straight ahead
                        aiAgent.transform.LookAt(spawnPostionsParent.transform.position);
                        aiObjects[i].SetActive(true);
                        IsAiAliveDictionary[aiObjects[i]] = true;
                        aiAgent.SetDestination(spawnPostions[UnityEngine.Random.Range(0, spawnPostions.Length)].position, spawnPostionsParent.transform.position);
                        _CurrentOnBoard++;
                    }
                }
                if (IsAiAliveDictionary[aiObjects[i]] && aiAgent.IsAtDestination())
                {
                    // despwan the AI object
                    DisableAI(aiObjects[i]);
                }
                yield return null;
            }
            yield return null;
        }
    }

    void Shuffle(Transform[] array)
    {
        int n = array.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            Transform temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

}
