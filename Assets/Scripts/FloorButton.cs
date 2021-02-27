using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{

    public Vector3 activateOffset;

    public InteractableObject[] affectedObjects;

    private Vector3 startPos;
    private Vector3 endPos;

    private bool activated = false;

    private void Start()
    {
        startPos = transform.position;
        endPos = startPos + activateOffset;
    }

    public void Activate()
    {
        if (!activated)
        {
            transform.position = endPos;
            FindObjectOfType<AudioManager>().Play("Button");
            foreach (InteractableObject obj in affectedObjects)
            {
                obj.active = !obj.active;
            }
        }
    }

}
