using System.Collections;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    private float currentTime = 0f;
    private int seconds;
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        seconds = (int)(currentTime % 60);
        Debug.Log(seconds);
    }


    void MakeWingTime()
    {
        int wingTimer = 3;
        if (seconds == wingTimer)
        {
            Debug.Log("GAME OVER");
        }
    }

    void TableTime()
    {
        int tableTimer = 5;
        if (seconds == tableTimer)
        {
            Debug.Log("GAME OVER");
        }
    }

    void WaitTime()
    {
        int waitTimer = 7;
        if (seconds == waitTimer)
        {
            Debug.Log("GAME OVER");
        }
    }
}
