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
            Milliseconds = Milliseconds + 1;
            if (Milliseconds == 60)
            {
                Seconds = Seconds + 1;
                Milliseconds = 0;
            }
            if (Seconds == 60)
            {
                Minutes = Minutes + 1;
                Seconds = 0;
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
