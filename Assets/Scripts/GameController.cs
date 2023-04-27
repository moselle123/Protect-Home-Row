using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameOver gameOverScreen;

    string currentSystem;
    bool shieldDown = false;
    bool engineDown = false;
    bool batteryDown = false;
    bool autopilotDown = false;
    bool oxygenDown = false;

    private bool isGamePaused = false;
    private float previousTimeScale;


    public void GameOver(string deathReason)
    {
        gameOverScreen.SetUp(deathReason);
    }

    public void setShieldDown(bool broke)
    {
        shieldDown = broke;
    }

    public bool getShieldDown()
    {
        return shieldDown;
    }

    public void setEngineDown(bool broke)
    {
        engineDown = broke;
    }

    public bool getEngineDown()
    {
        return engineDown;
    }

    public void setBatteryDown(bool broke)
    {
        batteryDown = broke;
    }

    public bool getBatteryDown()
    {
        return batteryDown;
    }

    public void setOxygenDown(bool broke)
    {
        oxygenDown = broke;
    }

    public bool getOxygenDown()
    {
        return oxygenDown;
    }

    public void setAutopilotDown(bool broke)
    {
        autopilotDown = broke;
    }

    public bool getAutopilotDown()
    {
        return autopilotDown;
    }

    public void setSystem(string system)
    {
        currentSystem = system;
    }
    
    public string GetSystem()
    {
        return currentSystem;
    }

    public void PauseGame()
    {
        if (!isGamePaused)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            isGamePaused = true;
        }
    }

    public void ResumeGame()
    {
        if (isGamePaused)
        {
            Time.timeScale = previousTimeScale;
            isGamePaused = false;
        }
    }

}
