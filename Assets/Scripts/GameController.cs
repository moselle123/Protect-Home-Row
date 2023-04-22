using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameOver gameOverScreen;
    string currentSystem;
    bool shieldDown = false;

    public void GameOver(string deathReason)
    {
        gameOverScreen.SetUp(deathReason);
    }

    public void setShieldDown(bool broke)
    {
        shieldDown = broke;
    }

    public void setSystem(string system)
    {
        currentSystem = system;
    }
}
