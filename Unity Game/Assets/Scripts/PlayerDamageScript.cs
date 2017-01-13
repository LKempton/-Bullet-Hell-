using UnityEngine;
using System.Collections;

public class PlayerDamageScript : MonoBehaviour {

    [SerializeField]
    private int playerHealth = 3;

    [SerializeField]
    private GameObject gameoverPanel;
    [SerializeField]
    private GameObject shield;

    private bool m_isShielded = false;

    [SerializeField]
    private Color baseColour;

    [SerializeField]
    private Color damageColour;

    [SerializeField]
    private float graceTime = 1.0f;

    private bool m_isDamageable = true;

    [SerializeField]
    private GameObject[] heartUI = new GameObject[3];

    [SerializeField]
    private GameObject damageTint;

    void Update()
    {
        if (playerHealth <= 0)
        {
            Die();
        }

        if (Input.GetButtonDown("Restart"))
        {
            RestartGame();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        // Checks if the player is hit by a bullet and is damageable.
        if (col.CompareTag("EnemyBullet") && m_isDamageable == true && m_isShielded == false)
        {
            m_isDamageable = false;
            StartCoroutine(GracePeriod());

            playerHealth -= 1;

            // Change the health UI.
            ChangeHealthHeart(true);

            StartCoroutine("DamageColour");

            Debug.Log(playerHealth);
        }
    }

    void ChangeHealthHeart(bool isRemoving)
    {
        if (isRemoving == true)
        {
            switch (playerHealth)
            {
                case 2:
                    heartUI[2].SetActive(false);
                    break;
                case 1:
                    heartUI[1].SetActive(false);
                    break;
                case 0:
                    heartUI[0].SetActive(false);
                    break;
            }
        }
        else if (isRemoving == false)
        {
            heartUI[playerHealth - 1].SetActive(true);
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
        damageTint.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        damageTint.SetActive(false);
    }

    IEnumerator GracePeriod()
    {
        // Makes the player not damageable for a short period after being hit.
        yield return new WaitForSeconds(graceTime);

        m_isDamageable = true;
    }

    public void SetPlayerHealth(int val)
    {
        if (playerHealth < 3)
        {
            Debug.Log("player health up");
            playerHealth += val;

            ChangeHealthHeart(false);
        }
    }

    public void SetShieldStatus(int val)
    {
        // Puts a shield on the enemy making them invinceable for a short time.
        m_isShielded = true;
        shield.SetActive(true);

        StopCoroutine("ShieldTime");
        StartCoroutine(ShieldTime(val));
    }

    IEnumerator ShieldTime(int amount)
    {
        yield return new WaitForSeconds(amount);
        m_isShielded = false;
        shield.SetActive(false);
    }
}
