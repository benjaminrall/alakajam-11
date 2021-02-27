using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableDoor : InteractableObject
{
    public int doorState = 0;

    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void ActivateToggle()
    {
        if (doorState <= 0)
        {
            doorState = 1;
        }
        else
        {
            doorState = -1;
        }
    }

    private void Update()
    {
        animator.SetInteger("DoorState", doorState);
    }
}
