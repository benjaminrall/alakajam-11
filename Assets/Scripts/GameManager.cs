using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    private PlayerController player;
    private CameraController cam;

    [Header("General")]
    public Image fade;

    [Header("Game Start")]
    public float shakeDuration;
    public float shakeMagnitude;
    public float shakeSpeed;
    private bool activated1 = false;
    public Animator rockHandler;

    [Header("Fall Sequence")]
    public Vector3[] rockSpawns;
    public GameObject[] rocks;

    [Header("Checkpoints")]
    public GameObject[] rooms;
    public GameObject[] checkpoints;
    public int currentRoom = -1;
    private GameObject storedRoom;
    private GameObject activeRoom;

    private List<GameObject> spawnedRocks;

    private void Start()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().UpdateBrightness(GameObject.Find("AudioManager").GetComponent<AudioManager>().currentBrightness);

        fade.gameObject.SetActive(true);

        player = FindObjectOfType<PlayerController>();
        cam = FindObjectOfType<CameraController>();

        spawnedRocks = new List<GameObject>(rockSpawns.Length);

        StartCoroutine(BeginningFade());

        currentRoom = -1;
    }

    private IEnumerator BeginningFade()
    {
        PlayerMovement playerMovement = player.gameObject.GetComponent<PlayerMovement>();

        while (!playerMovement)
        {
            playerMovement = player.gameObject.GetComponent<PlayerMovement>();
        }

        playerMovement.velocity = 0.0f;
        playerMovement.enabled = false;

        yield return new WaitForSeconds(1.0f);

        FadeOut(2.0f);

        playerMovement.enabled = true;
    }

    public void FadeIn(float delay)
    {
        fade.CrossFadeAlpha(1, delay, false);
    }

    public void FadeOut(float delay)
    {
        fade.CrossFadeAlpha(0, delay, false);
    }

    public IEnumerator GameStart()
    {
        if (!activated1)
        {
            Rumble();

            PlayerMovement playerMovement = player.gameObject.GetComponent<PlayerMovement>();

            playerMovement.velocity = 0.0f;
            playerMovement.enabled = false;

            yield return new WaitForSeconds(1.0f);

            Quaternion newRotation = Quaternion.AngleAxis(0, Vector3.up);

            rockHandler.SetBool("Play", true);
            FindObjectOfType<AudioManager>().Play("CaveIn");    

            while (player.transform.localRotation != new Quaternion(0, 0, 0, -1) && player.transform.localRotation != new Quaternion(0, 0, 0, 0) && player.transform.localRotation != new Quaternion(0, 0, 0, 1))
            {
                player.transform.localRotation = Quaternion.Slerp(player.transform.rotation, newRotation, Time.deltaTime * player.GetComponent<PlayerMovement>().turnTime);
                yield return null;
            }


            yield return new WaitForSeconds(2.0f);
            playerMovement.enabled = true;
            cam.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            activated1 = true;
        }
    }

    public void Rumble()
    {
        StartCoroutine(cam.Shake(shakeDuration, shakeMagnitude, shakeSpeed));
        FindObjectOfType<AudioManager>().Play("VibrationRumble");
    }

    public IEnumerator FallSequence()
    {
        Rumble();

        PlayerMovement playerMovement = player.gameObject.GetComponent<PlayerMovement>();

        playerMovement.velocity = 0.0f;
        playerMovement.enabled = false;

        System.Random r = new System.Random();

        FindObjectOfType<AudioManager>().Play("CaveIn");

        foreach (Vector3 rockSpawn in rockSpawns)
        {
            Instantiate(rocks[r.Next(0, rocks.Length)], rockSpawn, new Quaternion(r.Next(0, 91), r.Next(0, 360), r.Next(0, 91), 1));
            yield return new WaitForSeconds(0.18f);
        }

        yield return new WaitForSeconds(0.5f);


        StartCoroutine(player.Die("Snap"));

        yield return null;
    }

    public void UpdateCheckpoint(GameObject checkpoint)
    {
        int newCheckpoint = Array.IndexOf(checkpoints, checkpoint);
        if (newCheckpoint > currentRoom)
        {
            currentRoom = newCheckpoint;
            activeRoom = rooms[currentRoom];
            storedRoom = Instantiate(rooms[currentRoom]);
            storedRoom.SetActive(false);
        }
    }

    public void ResetRoom()
    {
        if (storedRoom != null && activeRoom != null)
        {
            Destroy(activeRoom);
            GameObject newRoom = Instantiate(storedRoom);
            newRoom.SetActive(true);
            activeRoom = newRoom;
        }
    }
}
