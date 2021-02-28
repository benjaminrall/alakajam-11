using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public float deathFadeInDelay;
    public float deathFadeOutDelay;

    private Vector3 previousCheckpoint;

    private void Start()
    {
        previousCheckpoint = transform.position;
        FindObjectOfType<AudioManager>().Play("TorchAmbient");
    }

    private void OnTriggerEnter (Collider other)
    {
        switch(other.tag)
        {
            case "Spikes": StartCoroutine(Die("SpikesDeath")); break;
            case "LogTrap": StartCoroutine(Die("LogTrapDeath")); break;
            case "FloorButton": other.gameObject.GetComponent<FloorButton>().Activate(); break;
            case "Torch": EnableChildren(other.transform); other.gameObject.GetComponent<Torch>().ActivateToggle(); break;
            case "GameStart": StartCoroutine(gameManager.GameStart()); break;
            case "FallSequence": break;
            case "PlayerSpikes": StartCoroutine(Die("SpikesDeath")); break;
        }
    }
    private IEnumerator Die(string type)
    {
        FindObjectOfType<AudioManager>().Play(type);
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<PlayerMovement>().velocity = 0.0f;
        gameManager.FadeIn(deathFadeInDelay);
        yield return new WaitForSeconds(deathFadeInDelay);
        transform.position = previousCheckpoint;
        yield return new WaitForSeconds(deathFadeOutDelay);
        gameManager.FadeOut(deathFadeOutDelay);
        yield return new WaitForSeconds(deathFadeOutDelay);
        gameObject.GetComponent<PlayerMovement>().enabled = true;
    }

    private void EnableChildren(Transform parent)
    {
        foreach(Transform child in parent)
        {
            child.gameObject.SetActive(true);
        }
    }
}
