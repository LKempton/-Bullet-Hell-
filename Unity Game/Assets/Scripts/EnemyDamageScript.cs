using UnityEngine;
using System.Collections;

public class EnemyDamageScript : MonoBehaviour {

    [SerializeField]
    private float health = 5f;
    [SerializeField]
    private int rndChance = 10;
    [SerializeField]
    private GameObject[] powerups = new GameObject[2];

    [SerializeField]
    private SpriteRenderer m_enemySprite;
    private Color m_defaultColour;

    [SerializeField]
    private Color damagedColour = new Color(0, 0, 0);

    private GameControllerScript m_gc;
    private SoundScript m_ss;
    private AlertScript m_alert;

    [SerializeField]
    private PointsSystemScript m_pointsScript;

    private GameObject m_player;

    // Marks whether the enemy is part of the larger elite enemy.
    [SerializeField]
    private bool m_isPartOfElite;

    // Marks whether the enemy is the main elite enemy.
    [SerializeField]
    private bool m_isElite;
    

    void Start()
    {
        m_gc = GameObject.FindWithTag("GameController").GetComponent<GameControllerScript>();
        m_alert = GameObject.FindWithTag("GameController").GetComponent<AlertScript>();
        m_ss = GameObject.FindWithTag("SoundManager").GetComponent<SoundScript>();
        m_pointsScript = GameObject.FindWithTag("PointsText").GetComponent<PointsSystemScript>();

        m_player = GameObject.FindWithTag("Player");

        m_enemySprite = GetComponentInChildren<SpriteRenderer>();

        if (m_isPartOfElite == false && m_enemySprite != null)
        {
            m_defaultColour = m_enemySprite.color;
        }
      
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Bullet"))
        {
            health -= 1;

            if (m_isPartOfElite == false)
            StartCoroutine("DamageColour");
        }
        else if (col.CompareTag("Rocket"))
        {
            health -= 5;
        }
    }

    void Update()
    {
        int rng;
        // Detects if the enemy health has dropped below 0 and destroys them if it has.
        if (health <= 0)
        {
            if (gameObject.layer == m_player.layer)
            {
                m_ss.PlaySoundClip(2);
            }

            if (m_pointsScript != null)
            {
                m_pointsScript.PointsUp(10);
            }
            else
            {
                m_pointsScript = GameObject.FindWithTag("PointsText").GetComponent<PointsSystemScript>();
            }
            
            rng = Random.Range(1, 100);
            
            if ((rng <= rndChance && m_gc.activePowerups == 0) || m_gc.dropNext == true)
            {
                SpawnPowerup();

                m_gc.PowerupDrop();
            }

            StopCoroutine("DamageColour");

            if (m_isPartOfElite == false)
            {
                m_gc.activeEnemies--;
            }
            else if (m_isElite == true)
            {
                m_gc.eliteActive = false;
            }
            
            AdjustAlert();

            Destroy(gameObject);
        }

        if (m_enemySprite == null && m_isPartOfElite == false)
        {
            m_enemySprite = GetComponentInChildren<SpriteRenderer>();
            m_defaultColour = m_enemySprite.color;
        }
    }

    private void SpawnPowerup()
    {
        // Spawn a random powerup on the enemies position.
        Instantiate(powerups[Random.Range(0, powerups.Length)], transform.position, transform.rotation);
    }

    private void AdjustAlert()
    {
        if (gameObject.layer == 10)
        {
            m_alert.RemoveEnemies(10);
        }
        else if (gameObject.layer == 9)
        {
            m_alert.RemoveEnemies(9);
        }
        else if (gameObject.layer == 8)
        {
            m_alert.RemoveEnemies(8);
        }
    }

    IEnumerator DamageColour()
    {
        m_enemySprite.color = damagedColour;

        yield return new WaitForSeconds(0.2f);

        m_enemySprite.color = m_defaultColour;
     
    }
}
