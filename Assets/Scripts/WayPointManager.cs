using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WayPointManager : MonoBehaviour
{
    public Camera mainCamera; 

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                NavMeshHit navMeshHit;
                if (NavMesh.SamplePosition(hit.point, out navMeshHit, 2.0f, NavMesh.AllAreas))
                {
                    BoardActionsEvents.WayPointChangeEvent.Invoke(navMeshHit.position);
                }
            }
        }
    }
}
