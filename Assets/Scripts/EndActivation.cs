using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndActivation : InteractableObject
{
    public GameManager gameManager;
    public TimerScript timerScript;

    public override void ActivateToggle()
    {
        StartCoroutine(LoadEndScene());
    }

    private IEnumerator LoadEndScene()
    {
        gameManager.FadeIn(1.0f);

        timerScript.StopTimer();

        yield return new WaitForSeconds(1.0f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(2);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
