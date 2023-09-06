using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class HealthBarComponent : ProgressBar
{
    public new class UxmlFactory : UxmlFactory<HealthBarComponent, UxmlTraits> { }
    public new class UxmlTrates : UxmlFactory<HealthBarComponent, UxmlTraits>
    {

    }
}
