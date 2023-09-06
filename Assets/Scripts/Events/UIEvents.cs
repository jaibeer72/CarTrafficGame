using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class UIEvents 
{
    public static UnityEvent<string> UIChangeEvent = new UnityEvent<string>();
    public static UnityEvent<string , Action> SceneAddEvent = new UnityEvent<string , Action>();
    public static UnityEvent<string , Action> SceneRemoveEvent = new UnityEvent<string , Action>();
}
