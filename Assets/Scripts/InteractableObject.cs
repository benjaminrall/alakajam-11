using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    public bool active = true;

    public virtual void ActivateToggle()
    {
        active = !active;
    }

}
