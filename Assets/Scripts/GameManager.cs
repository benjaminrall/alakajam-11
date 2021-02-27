using UnityEngine;
using UnityEngine.UI;

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

    public void GameStart()
    {
        if (!activated1)
        {
            StartCoroutine(cam.Shake(shakeDuration, shakeMagnitude, shakeSpeed));
            activated1 = true;
        }
    }
}
