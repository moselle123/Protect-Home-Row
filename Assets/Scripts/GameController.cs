using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameOver gameOverScreen;
    public GameOver levelComplete;

    public CombatTyper combatTyper;
    public PlayerCombat playerCombat;

    string currentSystem;
    bool shieldDown = false;
    bool engineDown = false;
    bool batteryDown = false;
    bool autopilotDown = false;
    bool oxygenDown = false;

    public GameObject combat;
    public GameObject game;

    string powerUp = null;
    Vector3 pickUpLocation;
    Quaternion pickUpRotation;
    public GameObject hammerPrefab;
    public GameObject scythePrefab;

    public GameObject mainCamera;
    public GameObject combatCamera;

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

    public void LevelComplete(string deathReason)
    {
        levelComplete.SetUp(deathReason);
    }

    public void setCombat(GameObject enemy)
    {
        pickUpLocation = enemy.transform.position;
        pickUpRotation = enemy.transform.rotation;
        Destroy(enemy);
        mainCamera.SetActive(false);
        combatCamera.SetActive(true);
        combat.SetActive(true);
        game.SetActive(false);
    }

    public void setMainGame()
    {
        combatCamera.SetActive(false);
        mainCamera.SetActive(true);
        combat.SetActive(false);
        game.SetActive(true);
        int randomi = Random.Range(1, 3);
        if (randomi == 1)
        {
            Instantiate(hammerPrefab, pickUpLocation, pickUpRotation);
        }
        else
        {
            Instantiate(scythePrefab, pickUpLocation, pickUpRotation);
        }
        powerUp = null;
        combatTyper.setWindow();
    }

    public void setPowerUp(string p)
    {
        powerUp = p;
        if (name == "hammer")
        {
            playerCombat.setHammer();
        }
        else
        {
            playerCombat.setScythe();
        }
    }

    public string getPowerUp()
    {
        return powerUp;
    }

}
