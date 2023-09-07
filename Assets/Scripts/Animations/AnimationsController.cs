using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    public GameObject ExplosionPrefab;

    private void Start()
    {
        AIEvents.DespwnAI.AddListener(OnAIDespwan);
    }

    private void OnAIDespwan(GameObject arg0)
    {
        // instantiate the explosation get the play the explosion and then
        GameObject explosion = Instantiate(ExplosionPrefab, arg0.transform.position, Quaternion.identity);
        ParticleSystem system = explosion.GetComponent<ParticleSystem>();
        system.Play();
    }
    private void OnDestroy()
    {
        AIEvents.DespwnAI.RemoveListener(OnAIDespwan); 
    }
}
