using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timers : MonoBehaviour
{
    public TextMeshProUGUI mainTimerText;
    public TextMeshProUGUI shieldTimerText;
    public TextMeshProUGUI engineTimerText;
    public TextMeshProUGUI autopilotTimerText;

    CountdownTimer mainTimer;
    CountdownTimer autopilot;
    CountdownTimer engine;
    CountdownTimer shield;

    List<CountdownTimer> currentTasks = new List<CountdownTimer>();

    float levelTime = 120f;
    float taskTime = 40f;

    void Start()
    {
        mainTimer = new CountdownTimer(mainTimerText, levelTime, true);
        shield = new CountdownTimer(shieldTimerText, taskTime, false);
        autopilot = new CountdownTimer(autopilotTimerText, taskTime, false);
        engine = new CountdownTimer(engineTimerText, taskTime, false);
        currentTasks.Add(shield);
        mainTimer.Go();
        shield.Go();
    }

    void Update()
    {
        mainTimer.Update();

        if (mainTimer.GetTime() < 90f && !autopilot.getGo())
        {
            currentTasks.Add(autopilot);
            autopilot.Go();
        }

        if (mainTimer.GetTime() < 60 && !engine.getGo())
        {
            currentTasks.Add(engine);
            engine.Go();
        }

        for (int i = 0; i < currentTasks.Count; i++)
        {
            currentTasks[i].Update();
            if (currentTasks[i].GetTime() < 0)
            {
                string deathReason = "";
                if (currentTasks[i] == autopilot)
                {
                    deathReason = "Death by crash!! Your autopilot system wasn't fixed in time!";
                    FindObjectOfType<GameController>().GameOver(deathReason);
                }
                if (currentTasks[i] == engine)
                {
                    deathReason = "Your engine shut down, you'll never make it to safety now!";
                    FindObjectOfType<GameController>().GameOver(deathReason);
                }
                if (currentTasks[i] == shield)
                {
                    FindObjectOfType<GameController>().setShieldDown(true);
                }
            }
        }  
    }

    public void taskComplete(CountdownTimer task)
    {
        task.ResetToOk();
        currentTasks.Remove(task);
    }
}
