using UnityEngine;
using System.Collections;

public class GameControllerScript : MonoBehaviour {

    [SerializeField]
    private float waitTime = 15f;

    public int activeEnemies = 0;
    public int activePowerups = 0;
    public bool eliteActive = false;

    public bool dropNext = false;

    void Start()
    {
        Time.timeScale = 1.0f;

        PowerupDrop();
    }

    // Waits a time of not having a powerup drop before forcing one on the next kill.
    public void PowerupDrop()
    {
        dropNext = false;
        StopCoroutine("DropWait");

        StartCoroutine("DropWait");
        
    }

    IEnumerator DropWait()
    {
        yield return new WaitForSeconds(waitTime);

        dropNext = true;
    }

    
}
