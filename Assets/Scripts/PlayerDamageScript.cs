using UnityEngine;
using System.Collections;

public class PlayerDamageScript : MonoBehaviour {

    [SerializeField]
    private int playerHealth = 3;

    [SerializeField]
    private GameObject gameoverPanel;

    [SerializeField]
    private Color baseColour;

    [SerializeField]
    private Color damageColour;

    [SerializeField]
    private GameObject[] heartUI = new GameObject[3];

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

            ChangeHealthHeart(false);
        }
    }
}
