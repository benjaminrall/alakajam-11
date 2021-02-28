using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapdoor : InteractableObject
{

    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("open", active);
    }
    public override void ActivateToggle()
    {
        base.ActivateToggle();
        animator.SetBool("open", active);
    }
}
