using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameOver gameOverScreen;
    bool shieldDown = false;

    public void GameOver(string deathReason)
    {
        gameOverScreen.SetUp(deathReason);
    }

    public void setShield(bool broke)
    {
        shieldDown = broke;
    }
}
