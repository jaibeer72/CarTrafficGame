using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class AIEvents 
{
    public static UnityEvent<AIEventSpwanArgs> SpwnAI = new UnityEvent<AIEventSpwanArgs>(); 
    public static UnityEvent<GameObject> DespwnAI = new UnityEvent<GameObject>();
    public static UnityEvent<bool> StopSpwanEnumirator = new UnityEvent<bool>();

}

public class AIEventSpwanArgs
{
    Vector3 spawnPosition;
    Vector3 endPosition; 
    public AIEventSpwanArgs(Vector3 spawnPosition, Vector3 endPosition)
    {
        this.spawnPosition = spawnPosition;
        this.endPosition = endPosition; 
    }
}