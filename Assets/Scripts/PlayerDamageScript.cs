using UnityEngine;
using System.Collections;

public class PlayerDamageScript : MonoBehaviour {

    [SerializeField]
    private float playerHealth = 3.0f;

    [SerializeField]
    private GameObject gameoverPanel;

    void Update()
    {
        if (playerHealth <= 0)
        {
            Die();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("EnemyBullet"))
        {
            playerHealth -= 1;

            Debug.Log(playerHealth);
        }
    }

    void Die()
    {
        gameoverPanel.SetActive(true);

        Time.timeScale = 0.0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;

        Application.LoadLevel(Application.loadedLevel);
    }
}
