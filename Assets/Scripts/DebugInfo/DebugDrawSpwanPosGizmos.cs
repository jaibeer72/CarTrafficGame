using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class DebugDrawSpwanPosGizmos : MonoBehaviour
{
    GameObject spawnPositionParent;
    Transform[] spawnPositionTransforms;

    void Awake()
    {
        spawnPositionParent = this.gameObject;
        spawnPositionTransforms = spawnPositionParent.GetComponentsInChildren<Transform>();

        foreach (Transform spawnTransform in spawnPositionTransforms)
        {
            if (spawnTransform == spawnPositionParent.transform)
                continue;

            if(spawnTransform.gameObject.GetComponent<DrawSpawnPosGizmos>() == null)
            {
                spawnTransform.gameObject.AddComponent<DrawSpawnPosGizmos>();
            } 
        }
    }

    private void Update()
    {

    }
}