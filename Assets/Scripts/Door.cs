using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable_MasterClass
{
    private bool _IsDoorOpen = false;
    [SerializeField]private Animator _Animator;
    public override void Activate()
    {
        Debug.Log("This is a " + name);
        if(_Animator != null)
        {
            //if the door is closed play the open animation if the door is open play the closed animation
            _Animator.Play(_IsDoorOpen ? "TransitionDoorClosing_Anim" : "TransitionDoorOpening_Anim");
            //once the animation plays check if it is set to false, if it is set to true. if it is true, set to false
            _IsDoorOpen = _IsDoorOpen ? false : true;
            Debug.Log(_IsDoorOpen);
        }
        else
        {
            Debug.LogWarning("ANIMATOR NOT SET");
        }
        
    }
}
