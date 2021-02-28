using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public int Milliseconds;
    public int Seconds;
    public int Minutes;

    public bool TimerActive = true;

    public string FinalTime;

    void Start()
    {
        DontDestroyOnLoad(this);
        TimerActive = true;
    }
    void FixedUpdate()
    {
        if (TimerActive == true)
        {
            Milliseconds -= 1;
            if (Milliseconds <= 0)
            {
                Seconds -= 1;
                Milliseconds = 59;
            }
            if (Seconds <= 0)
            {
                Minutes -= 1;
                Seconds = 59;
            }
        }
    }

    public void StopTimer()
    {
        TimerActive = false;
        if (Seconds < 10)
        {
            FinalTime = Minutes.ToString() + ":0" + Seconds.ToString();
        }
        else
        {
            FinalTime = Minutes.ToString() + ":" + Seconds.ToString();
        }
    }
}
