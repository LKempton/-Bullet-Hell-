using UnityEngine;
using System.Collections;

public class PowerupBehaviourScript : MonoBehaviour {

    [SerializeField]
    private int powerupType = 0;
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private float despawnTime = 10.0f;
    private float timeCreated;

    private Rigidbody powerupRB;

    private Color bottomLayer = new Color(0.0f, 0.0f, 255f);
    private Color middleLayer = new Color(255f, 255f, 0.0f);
    private Color topLayer = new Color(255f, 0.0f, 0.0f);

    private PlayerDamageScript pd;
    private GameControllerScript gc;
    private LK.PlayerShootingScript ps;

    void Start()
    {
        // Find and make reference to the scripts at start so they can be accessed later.
        pd = GameObject.FindWithTag("Player").GetComponent<PlayerDamageScript>();
        ps = GameObject.FindWithTag("Player").GetComponent<LK.PlayerShootingScript>();
        gc = GameObject.FindWithTag("GameController").GetComponent<GameControllerScript>();

        powerupRB = GetComponent<Rigidbody>();

        gc.activePowerups++;

        SelectColour();

        powerupRB.velocity = -transform.right * speed;

        timeCreated = Time.time;
    }

    void Update()
    {
        // If the powerup has been active lonnger than the activeTime allows destroy it.
        if (despawnTime < Time.time - timeCreated)
        {
            gc.activePowerups--;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            switch (powerupType)
            {
                case 0:
                    HealthUp();
                    gc.activePowerups--;
                    Destroy(gameObject);
                    break;

                case 1:
                    ShieldUp();
                    gc.activePowerups--;
                    Destroy(gameObject);
                    break;
                    
                case 2:
                    DoubleShot();
                    gc.activePowerups--;
                    Destroy(gameObject);
                    break;
            }
        }
    }

    void HealthUp()
    {
        pd.SetPlayerHealth(1);
    }

    void ShieldUp()
    {
        pd.SetShieldStatus();
    }

    void DoubleShot()
    {
        ps.DoubleShot();
    }


    void SelectColour()
    {
        if (transform.position.y == 0)
        {
            gameObject.GetComponent<Renderer>().material.color = bottomLayer;
        }
        else if (transform.position.y == 10)
        {
            gameObject.GetComponent<Renderer>().material.color = middleLayer;
        }
        else if (transform.position.y == 20)
        {
            gameObject.GetComponent<Renderer>().material.color = topLayer;
        }
    }
}
