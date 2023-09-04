using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class CollectablesEvents
{
    public static UnityEvent<GameObject> PlayerCollected = new UnityEvent<GameObject>();
}
