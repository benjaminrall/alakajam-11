using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : InteractableObject
{

    public float expandTime;
    public float retractTime;
    public float loopDelay;

    public Vector3 expandOffset;

    private bool expanded = false;

    private Vector3 startPos;
    private Vector3 endPos;

    private void Start()
    {
        startPos = transform.position;
        endPos = startPos + expandOffset;
        StartCoroutine(ActiveSpikes());
    }

    private IEnumerator ActiveSpikes()
    {
        while (active || expanded)
        {
            if (!expanded)
            {
                FindObjectOfType<AudioManager>().PlayAt("Spikes", transform.position);
                transform.position = endPos;
                GetComponent<BoxCollider>().enabled = true;
                yield return new WaitForSeconds(expandTime);
                expanded = true;
            }
            else
            {
                yield return new WaitForSeconds(retractTime);
                transform.position = startPos;
                GetComponent<BoxCollider>().enabled = false;
                expanded = false;
                yield return new WaitForSeconds(loopDelay);
            }
        }
        
    }

}
