using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogTrap : InteractableObject
{

    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("active", active);
    }

    public override void ActivateToggle()
    {
        base.ActivateToggle();
        animator.SetBool("active", active);
    }
}
