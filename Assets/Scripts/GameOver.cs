using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI deathReason;

    public void SetUp(string _deathReason)
    {
        gameObject.SetActive(true);
        deathReason.text = _deathReason;
    }
}
