using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuHandler : MonoBehaviour
{

    public Image fade;
    public GameObject optionsMenu;
    public Slider volumeSlider;
    public Slider brightnessSlider;

    public AudioMixer audioMixer;

    private void Start()
    {
        fade.gameObject.SetActive(true);
        fade.canvasRenderer.SetAlpha(0.0f);
        optionsMenu.SetActive(false);
    }

    public void Play()
    {
        StartCoroutine(LoadGame());
    }

    private IEnumerator LoadGame()
    {
        fade.CrossFadeAlpha(1, 1.0f, false);

        yield return new WaitForSeconds(2.0f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        while (!operation.isDone)
        {
            yield return null;
        }
    }

    public void ToggleOptionsMenu()
    {
        if (optionsMenu.activeInHierarchy) optionsMenu.SetActive(false);
        else optionsMenu.SetActive(true);
    }

    public void UpdateBrightness()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().UpdateBrightness(brightnessSlider.value);
    }
    public void UpdateVolume(float volume)
    {
        if (volume > -40)
            audioMixer.SetFloat("LikeMasterVolumeOrSomething", volume);
        else
            audioMixer.SetFloat("LikeMasterVolumeOrSomething", -80);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
