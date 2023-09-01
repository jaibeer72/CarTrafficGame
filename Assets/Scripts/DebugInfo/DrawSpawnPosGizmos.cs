using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DrawSpawnPosGizmos : MonoBehaviour
{
    public Color color = Color.green;
    public float radius = 0.5f;
    public bool selected = false;

    void OnDrawGizmos()
    {
        Gizmos.color = (selected) ? Color.red : color;
        Gizmos.DrawSphere(transform.position, radius);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
