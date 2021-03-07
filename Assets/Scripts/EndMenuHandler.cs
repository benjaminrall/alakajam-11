using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenuHandler : MonoBehaviour
{

    public Image fade;
    public Text timerText;

    private void Awake()
    {
        if (FindObjectOfType<TimerScript>())
        {
            timerText.text = FindObjectOfType<TimerScript>().FinalTime;
        }
    }

    private void Start()
    {
        fade.gameObject.SetActive(true);
        fade.canvasRenderer.SetAlpha(0.0f);
    }
    public void LoadMenu()
    {
        Destroy(FindObjectOfType<TimerScript>());
        StartCoroutine(LoadGame());
    }

    private IEnumerator LoadGame()
    {
        fade.CrossFadeAlpha(1, 1.0f, false);

        yield return new WaitForSeconds(2.0f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(0);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
