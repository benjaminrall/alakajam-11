using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public float maxSpeed;
    
    public float acceleration;

    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }
}
