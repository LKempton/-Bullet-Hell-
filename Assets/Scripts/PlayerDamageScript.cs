using UnityEngine;
using System.Collections;

public class PlayerDamageScript : MonoBehaviour {

    [SerializeField]
    private float playerHealth = 3.0f;

    [SerializeField]
    private GameObject gameoverPanel;

    [SerializeField]
    private Color baseColour;

    [SerializeField]
    private Color damageColour;

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

            StartCoroutine("DamageColour");

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

    IEnumerator DamageColour()
    {
        gameObject.GetComponent<Renderer>().material.color = damageColour;

        yield return new WaitForSeconds(0.2f);

        gameObject.GetComponent<Renderer>().material.color = baseColour;
    }

    public void SetPlayerHealth(int val)
    {
        if (playerHealth < 3)
        {
            Debug.Log("player health up");
            playerHealth += val;
        }
    }
}
