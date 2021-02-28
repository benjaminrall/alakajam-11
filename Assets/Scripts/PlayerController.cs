using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public float deathFadeInDelay;
    public float deathFadeOutDelay;

    private bool hasJewel;
    private bool inJewel;

    public GameObject jewel;
    public GameObject playerJewel;

    public Vector3 startPos;

    private Vector3 previousCheckpoint;

    private void Start()
    {
        previousCheckpoint = startPos;


        hasJewel = false;
        inJewel = false;
    }

    private void Update()
    {
        if (inJewel && !hasJewel && Input.GetKeyDown(KeyCode.E))
        {
            hasJewel = true;
            gameManager.Rumble();
            GetJewel();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Spikes": StartCoroutine(Die("SpikesDeath")); break;
            case "LogTrap": StartCoroutine(Die("LogTrapDeath")); break;
            case "FloorButton": other.gameObject.GetComponent<FloorButton>().Activate(); break;
            case "Torch": EnableChildren(other.transform); other.gameObject.GetComponent<Torch>().ActivateToggle(); break;
            case "GameStart": StartCoroutine(gameManager.GameStart()); break;
            case "FallSequence": if (hasJewel) StartCoroutine(gameManager.FallSequence()); break;
            case "Jewel": if (!hasJewel) inJewel = true; break;
            case "PlayerSpikes": StartCoroutine(Die("SpikesDeath")); break;
            case "ToggleButton": other.gameObject.GetComponent<ToggleableButton>().Activate(); break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Jewel" && inJewel && !hasJewel) inJewel = false;
        else if (other.tag == "ToggleButton") StartCoroutine(other.gameObject.GetComponent<ToggleableButton>().Deactivate());
    }

    public IEnumerator Die(string type)
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
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void GetJewel()
    {
        jewel.SetActive(false);
        playerJewel.SetActive(true);
    }
}
