using UnityEngine;
using System.Collections;

public class PowerupBehaviourScript : MonoBehaviour {

    [SerializeField]
    private int powerupType = 0;
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private float despawnTime = 10.0f;
    private float m_timeCreated;

    private Rigidbody m_powerupRB;

    private PlayerDamageScript m_pd;
    private GameControllerScript m_gc;
    private LK.PlayerShootingScript m_ps;
    private ChangeLayerScript m_cl;

    void Start()
    {
        // Find and make reference to the scripts at start so they can be accessed later.
        m_pd = GameObject.FindWithTag("Player").GetComponent<PlayerDamageScript>();
        m_ps = GameObject.FindWithTag("Player").GetComponent<LK.PlayerShootingScript>();
        m_gc = GameObject.FindWithTag("GameController").GetComponent<GameControllerScript>();
        m_cl = GameObject.FindWithTag("GameController").GetComponent<ChangeLayerScript>();

        m_powerupRB = GetComponent<Rigidbody>();

        m_gc.activePowerups++;

        m_powerupRB.velocity = -transform.right * speed;

        m_timeCreated = Time.time;

        m_cl.SetLayerRecursively(gameObject, SelectLayer());
    }

    void Update()
    {
        // If the powerup has been active lonnger than the activeTime allows destroy it.
        if (despawnTime < Time.time - m_timeCreated)
        {
            m_gc.activePowerups--;
            Destroy(gameObject);
        }
    }

    // Detects if the player touches a powerup.
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            switch (powerupType)
            {
                case 0:
                    HealthUp();
                    m_gc.activePowerups--;
                    Destroy(gameObject);
                    break;

                case 1:
                    ShieldUp();
                    m_gc.activePowerups--;
                    Destroy(gameObject);
                    break;
                    
                case 2:
                    DoubleShot();
                    m_gc.activePowerups--;
                    Destroy(gameObject);
                    break;
            }
        }
    }

    void HealthUp()
    {
        m_pd.SetPlayerHealth(1);
    }

    void ShieldUp()
    {
        m_pd.SetShieldStatus(5);
    }

    void DoubleShot()
    {
        m_ps.DoubleShot();
    }


    private int SelectLayer()
    {
        if (transform.position.y == 0)
        {
            return 10;
        }
        else if (transform.position.y == 10)
        {
            return 9;
        }
        else if (transform.position.y == 20)
        {
            return 8;
        }
        return 10;
    }
}
