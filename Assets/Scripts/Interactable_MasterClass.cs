using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable_MasterClass : MonoBehaviour, I_Interactable
{
    public void Click()
    {
        Activate();
    }

    public abstract void Activate();
}
