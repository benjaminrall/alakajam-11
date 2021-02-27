using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimController : MonoBehaviour
{
    private Rigidbody player;
    private Animator anim;
    private float currentVelocity;
    public float adjustmentThingy;
    public float movementThreshold = 0.1f;
    void Start()
    {
        player = GetComponentInParent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        currentVelocity = Math.Abs(player.velocity.x);
        anim.SetFloat("PlayerSpeed", currentVelocity * adjustmentThingy);

        if (currentVelocity > movementThreshold) anim.SetBool("IsMoving", true);
        else anim.SetBool("IsMoving", false);
    }
}
