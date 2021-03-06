using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour
{
    public GameObject optionsMenu;
    public Slider volumeSlider;
    public Slider brightnessSlider;

    public AudioMixer audioMixer;

    void Start()
    {
        optionsMenu.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) optionsMenu.SetActive(!optionsMenu.activeInHierarchy);
    }

    public void ClosePauseMenu()
    {
        optionsMenu.SetActive(false);
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
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
