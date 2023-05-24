using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int minutes;
    public int seconds;
    public bool newMinute;
    // Start is called before the first frame update
    void Start()
    {
        minutes = 4;
        seconds = 50;
        newMinute = false;
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            seconds++;
            if (seconds == 60)
            {
                minutes++;
                seconds = 0;
                newMinute = true;
            }
            Debug.Log("Minutes: " + minutes + " Seconds: " + seconds);
        }
    }
}
