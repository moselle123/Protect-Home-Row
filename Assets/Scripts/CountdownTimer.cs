using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimer
{
    float currentTime = 0f;
    float startingTime;
    public TextMeshProUGUI output;
    bool go = false;
    bool main;

    public CountdownTimer(TextMeshProUGUI _output, float _startingTime, bool _main)
    {
        output = _output;
        startingTime = _startingTime;
        main = _main;
    }

    public void Go()
    {
        currentTime = startingTime;
        go = true;
    }

    public void Update()
    {
        if (go)
        {
            currentTime -= Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            output.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);

            if (currentTime < 30)
            {
                output.color = Color.red;
            }
        }
        else
        {
            output.color = Color.green;
            output.text = "OK";
        }
    }

    public void Penalty(float penalty)
    {
        currentTime -= penalty;
    }

    public void Stop()
    {
        currentTime = startingTime;
    }

    public float GetTime()
    {
        return currentTime;
    }

    public void shieldBroken()
    {
        output.color = Color.red;
        output.text = "DOWN";
    }

    public void ResetToOk()
    {
        output.text = "OK";
        output.color = Color.green;
    }

    public bool getGo()
    {
        return go;
    }

    public void setShieldBroken()
    {
        go = false;
        output.text = "DOWN";
    }
}
