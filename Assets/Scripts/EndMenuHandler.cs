using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenuHandler : MonoBehaviour
{

    public Text timerText;

    public void Awake()
    {
        timerText.text = GameObject.FindObjectOfType<TimerScript>().FinalTime;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
