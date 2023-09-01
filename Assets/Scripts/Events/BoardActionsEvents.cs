using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class BoardActionsEvents
{
    public static UnityEvent<Vector3> WayPointChangeEvent = new UnityEvent<Vector3>();
}
