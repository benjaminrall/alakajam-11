using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    public float maxWalkingSpeed;
    public float maxRunningSpeed;
    
    //public float accelerationTime;
    public float decelerationTime;

    public float turnTime;

    public float footstepDelay;
    private float footstepTimer;
    
    public float acceleration;
    private float deceleration;

    [HideInInspector]
    public float velocity = 0.0f;

    private float maxVelocity;

    private bool running;
    private float ax;

    private bool decelerating;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        maxVelocity = maxWalkingSpeed;
        deceleration = maxVelocity / (decelerationTime);
        running = false;
        ax = 0.0f;
        decelerating = false;
    }

    private void Update()
    {
        running = Input.GetButton("KSprint") || Input.GetAxis("CSprint") > 0;
        ax = Input.GetAxis("Horizontal");

        if (running) 
        { 
            maxVelocity = maxRunningSpeed; 
            decelerating = false; 
        }
        else 
        { 
            maxVelocity = maxWalkingSpeed; 
            if ((velocity > 0 && velocity > maxVelocity * ax) || (velocity < 0 && velocity < maxVelocity * ax)) decelerating = true; 
        }

        // Positive Acceleration
        if (ax > 0 && velocity >= 0 && !decelerating)
        {
            Quaternion newRotation = Quaternion.AngleAxis(0, Vector3.up);
            transform.localRotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * turnTime);
            velocity += acceleration * Time.deltaTime;
            if (velocity > maxVelocity * ax)
            {
                velocity = maxVelocity * ax;
            }
        }
        // Negative Acceleration
        else if (ax < 0 && velocity <= 0 && !decelerating)
        {
            Quaternion newRotation = Quaternion.AngleAxis(180, Vector3.up);
            transform.localRotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * turnTime);
            velocity -= acceleration * Time.deltaTime;
            if (velocity < maxVelocity * ax)
            {
                velocity = maxVelocity * ax;
            }
        }
        // Deceleration
        else if (decelerating || ax == 0 && velocity != 0 || ax > 0 && velocity < 0 || ax < 0 && velocity > 0)
        {
            if (velocity < 0)
            {
                velocity += deceleration;
                if (velocity > 0)
                {
                    velocity = 0;
                }
            }
            else if (velocity > 0)
            {
                velocity -= deceleration;
                if (velocity < 0)
                {
                    velocity = 0;
                }
            }
        }

        // Decelerating from sprint to walk
        if (decelerating && ((velocity > 0 && velocity < maxVelocity * ax) || (velocity < 0 && velocity > maxVelocity * ax) || velocity == 0))
            decelerating = false;

        rb.velocity = new Vector3(velocity, rb.velocity.y, rb.velocity.z);

        footstepTimer += Time.deltaTime;
        if (footstepTimer > footstepDelay && Mathf.Abs(velocity) > 0)
        {
            FindObjectOfType<AudioManager>().Play("StoneStep");
            footstepTimer = 0.0f;
        }
    }
}
