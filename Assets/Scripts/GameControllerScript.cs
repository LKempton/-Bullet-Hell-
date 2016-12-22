using UnityEngine;
using System.Collections;

public class GameControllerScript : MonoBehaviour {

    public int activeEnemies = 0;
    public int activePowerups = 0;

    public int dropChance = 10;

    void Start()
    {
        Time.timeScale = 1.0f;
    }

    
}
