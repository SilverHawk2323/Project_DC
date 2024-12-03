using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandDoor : Interactable_MasterClass
{
    private bool _IsDoorOpen = false;
    [SerializeField] private Animator _Animator, _Animator2;
    private AudioSource doorOpeningSFX;
    [SerializeField] private AudioClip doorSFX;

    private void Start()
    {
        doorOpeningSFX = GetComponent<AudioSource>();
    }

    public override void Activate()
    {
        Debug.Log("This is a " + name);
        if (_Animator != null && _Animator2 != null)
        {
            //if the door is closed play the open animation if the door is open play the closed animation
            _Animator.Play(_IsDoorOpen ? "Closing_Anim" : "Opening_Anim");
            _Animator2.Play(_IsDoorOpen ? "Closing_Anim" : "Opening_Anim");
            //once the animation plays check if it is set to false, if it is set to true. if it is true, set to false
            _IsDoorOpen = _IsDoorOpen ? false : true;
            Debug.Log(_IsDoorOpen);
            doorOpeningSFX.PlayOneShot(doorSFX);
        }
        else
        {
            Debug.LogWarning("ANIMATOR NOT SET");
        }

    }
}
