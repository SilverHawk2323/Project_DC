using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable_MasterClass
{
    private ParticleSystem pickupEffect;
    private bool doOnce;
    [SerializeField] GameObject pickupMesh;
    private AudioSource chestOpeningSFX;
    [SerializeField] private AudioClip chestSFX;

    private void Start()
    {
        pickupEffect = GetComponentInChildren<ParticleSystem>();
        chestOpeningSFX = GetComponent<AudioSource>();
    }
    public override void Activate()
    {
        if(doOnce)
        {
            return;
        }
        pickupEffect.Play();
        chestOpeningSFX.PlayOneShot(chestSFX);
        Destroy(pickupMesh);
        doOnce = true;
    }
}
