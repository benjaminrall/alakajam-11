using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Image fade;

    private void Start()
    {
        fade.gameObject.SetActive(true);
        fade.canvasRenderer.SetAlpha(0.0f);
    }

    public void FadeIn(float delay)
    {
        fade.CrossFadeAlpha(1, delay, false);
    }

    public void FadeOut(float delay)
    {
        fade.CrossFadeAlpha(0, delay, false);
    }
}
