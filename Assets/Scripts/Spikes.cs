using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : InteractableObject
{

    public float delay;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(ActiveSpikes());
    }

    public override void ActivateToggle()
    {
        base.ActivateToggle();
        if (active)
        {
            StartCoroutine(ActiveSpikes());
        }
    }

    private IEnumerator ActiveSpikes()
    {
        while (active)
        {
            animator.SetBool("expand", true);
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(1.0f);
            animator.SetBool("expand", false);
            yield return new WaitForSeconds(delay);
        }
    }
}
