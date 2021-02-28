using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{

    public Image fade;

    private void Start()
    {
        fade.gameObject.SetActive(true);
        fade.canvasRenderer.SetAlpha(0.0f);
    }

    public void Play ()
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

    public void Quit()
    {
        Application.Quit();
    }

}
