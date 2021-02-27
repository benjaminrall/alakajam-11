using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

    private void Start()
    {
        fade.gameObject.SetActive(true);
        fade.canvasRenderer.SetAlpha(0.0f);

        player = FindObjectOfType<PlayerController>();
        cam = FindObjectOfType<CameraController>();
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
            StartCoroutine(cam.Shake(shakeDuration, shakeMagnitude, shakeSpeed));

            PlayerMovement playerMovement = player.gameObject.GetComponent<PlayerMovement>();

            playerMovement.velocity = 0.0f;
            playerMovement.enabled = false;

            yield return new WaitForSeconds(1.0f);

            Quaternion newRotation = Quaternion.AngleAxis(0, Vector3.up);

            rockHandler.SetBool("Play", true);

            while (player.transform.localRotation != new Quaternion(0, 0, 0, -1))
            {
                player.transform.localRotation = Quaternion.Slerp(player.transform.rotation, newRotation, .05f);
                yield return null;
            }


            yield return new WaitForSeconds(3.0f);
            playerMovement.enabled = true;
            cam.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            activated1 = true;
        }
    }
}
