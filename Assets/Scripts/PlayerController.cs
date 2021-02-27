using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public float deathFadeDelay;

    private Vector3 previousCheckpoint;

    private void Start()
    {
        previousCheckpoint = transform.position;
    }

    private void OnTriggerEnter (Collider other)
    {
        switch(other.tag)
        {
            case "Spikes": StartCoroutine(Die("SpikesDeath")); break;
            case "FloorButton": other.gameObject.GetComponent<FloorButton>().Activate(); break;
        }
    }
    private IEnumerator Die(string type)
    {
        FindObjectOfType<AudioManager>().Play(type);
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<PlayerMovement>().velocity = 0.0f;
        gameManager.FadeIn(deathFadeDelay);
        yield return new WaitForSeconds(deathFadeDelay);
        transform.position = previousCheckpoint;
        yield return new WaitForSeconds(deathFadeDelay);
        gameManager.FadeOut(deathFadeDelay);
        yield return new WaitForSeconds(deathFadeDelay);
        gameObject.GetComponent<PlayerMovement>().enabled = true;
    }

}
