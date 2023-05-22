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
    public TextMeshProUGUI oxygenTimerText;
    public TextMeshProUGUI batteryTimerText;

    CountdownTimer mainTimer;
    CountdownTimer autopilot;
    CountdownTimer engine;
    CountdownTimer shield;
    CountdownTimer battery;
    CountdownTimer oxygen;

    List<CountdownTimer> currentTasks = new List<CountdownTimer>();

    float levelTime = 120f;
    float taskTime = 40f;

    void Start()
    {
        mainTimer = new CountdownTimer(mainTimerText, levelTime, true);
        shield = new CountdownTimer(shieldTimerText, taskTime, false);
        autopilot = new CountdownTimer(autopilotTimerText, taskTime, false);
        engine = new CountdownTimer(engineTimerText, taskTime, false);
        battery = new CountdownTimer(batteryTimerText, taskTime, false);
        oxygen = new CountdownTimer(oxygenTimerText, taskTime, false);
        mainTimer.Go();
        shield.setShieldBroken();
        FindObjectOfType<GameController>().setShieldDown(true);        
    }

    void Update()
    {
        if (mainTimer.GetTime() <= 0)
        {
            FindObjectOfType<GameController>().LevelComplete("nothing");
        }
        mainTimer.Update();

        if (FindObjectOfType<GameController>().getAutopilotDown() && !autopilot.getGo())
        {
            autopilot.Go();
            currentTasks.Add(engine);
        }
        if (FindObjectOfType<GameController>().getEngineDown() && !engine.getGo())
        {
            engine.Go();
            currentTasks.Add(engine);
        }
        if (FindObjectOfType<GameController>().getOxygenDown() && !oxygen.getGo())
        {
            oxygen.Go();
            currentTasks.Add(oxygen);
        }
        if (FindObjectOfType<GameController>().getBatteryDown() && !battery.getGo())
        {
            battery.Go();
            currentTasks.Add(battery);
        }

        if (!FindObjectOfType<GameController>().getShieldDown())
        {
            shield.ResetToOk();
        }


        if (mainTimer.GetTime() < 90f && mainTimer.GetTime() > 85f && !autopilot.getGo())
        {
            currentTasks.Add(autopilot);
            FindObjectOfType<GameController>().setAutopilotDown(true);
            autopilot.Go();
        }

        if (mainTimer.GetTime() < 60 && mainTimer.GetTime() > 55f && !engine.getGo())
        {
            currentTasks.Add(engine);
            FindObjectOfType<GameController>().setEngineDown(true);
            engine.Go();
        }

        if (autopilot.getGo() && !FindObjectOfType<GameController>().getAutopilotDown())
        {
            taskComplete(autopilot);
        }

        if (engine.getGo() && !FindObjectOfType<GameController>().getEngineDown())
        {
            taskComplete(engine);
        }
        if (oxygen.getGo() && !FindObjectOfType<GameController>().getOxygenDown())
        {
            taskComplete(oxygen);
        }

        if (battery.getGo() && !FindObjectOfType<GameController>().getBatteryDown())
        {
            taskComplete(battery);
        }
        if (shield.getGo() && !FindObjectOfType<GameController>().getShieldDown())
        {
            taskComplete(shield);
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
                    currentTasks[i].setShieldBroken();
                    FindObjectOfType<GameController>().setShieldDown(true);
                }
                if (currentTasks[i] == oxygen)
                {
                    deathReason = "Your oxygen was shut down for too long, you died!";
                    FindObjectOfType<GameController>().GameOver(deathReason);
                }
                if (currentTasks[i] == battery)
                {
                    deathReason = "Your battery shut down nothing in the ship has power!";
                    FindObjectOfType<GameController>().GameOver(deathReason);
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
