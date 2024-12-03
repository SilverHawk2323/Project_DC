using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable_MasterClass
{
    private ParticleSystem pickupEffect;
    private bool doOnce;
    [SerializeField] GameObject pickupMesh;

    private void Start()
    {
        pickupEffect = GetComponentInChildren<ParticleSystem>();
    }
    public override void Activate()
    {
        if(doOnce)
        {
            return;
        }
        pickupEffect.Play();
        Destroy(pickupMesh);
        doOnce = true;
    }
}
