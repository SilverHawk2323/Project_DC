using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private Animator _Animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            _Animator.Play("SpikeUp");
        }
        Debug.Log("Spikes");
    }

    private void OnTriggerExit(Collider other)
    {
        _Animator.Play("SpikeDown");
    }
}
