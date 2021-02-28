using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateRope : InteractableObject
{
    public override void ActivateToggle()
    {
        if (!active) active = true;
        else gameObject.SetActive(false);
    }
}
