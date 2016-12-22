using UnityEngine;
using System.Collections;

public class EnemyDamageScript : MonoBehaviour {

    [SerializeField]
    private float health = 5f;
    [SerializeField]
    private int rndChance = 10;
    [SerializeField]
    private GameObject[] powerups = new GameObject[1];

    private Color bottomLayer = new Color(0.0f, 0.0f, 255f);
    private Color middleLayer = new Color(255f, 255f, 0.0f);
    private Color topLayer = new Color(255f, 0.0f, 0.0f);

    [SerializeField]
    private Color bottomLayerDamaged = new Color(115f, 115f, 255f);
    [SerializeField]
    private Color middleLayerDamaged = new Color(179f, 179f, 29f);
    [SerializeField]
    private Color topLayerDamaged = new Color(255f, 130f, 130f);

    private GameControllerScript gc;
    private Color currentColour;

    void Start()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<GameControllerScript>();

        currentColour = gameObject.GetComponent<Renderer>().material.color;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Bullet"))
        {
            health -= 1;

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
            rng = Random.Range(1, 100);
            Debug.Log(rng);
            if (rng <= rndChance)
            {
                SpawnPowerup();
            }

            StopCoroutine("DamageColour");

            gc.activeEnemies--;

            Destroy(gameObject);
        }
    }

    private void SpawnPowerup()
    {
        // Spawn a random powerup on the enemies position.
        Instantiate(powerups[Random.Range(0, powerups.Length - 1)], transform.position, transform.rotation);
    }

    IEnumerator DamageColour()
    {
        if (currentColour == topLayer)
        {
            gameObject.GetComponent<Renderer>().material.color = topLayerDamaged;

            yield return new WaitForSeconds(0.2f);

            gameObject.GetComponent<Renderer>().material.color = topLayer;
        }
        else if(currentColour == middleLayer)
        {
            gameObject.GetComponent<Renderer>().material.color = middleLayerDamaged;

            yield return new WaitForSeconds(0.2f);

            gameObject.GetComponent<Renderer>().material.color = middleLayer;
        }
        else if (currentColour == bottomLayer)
        {
            gameObject.GetComponent<Renderer>().material.color = bottomLayerDamaged;

            yield return new WaitForSeconds(0.2f);

            gameObject.GetComponent<Renderer>().material.color = bottomLayer;
        }
    }
}
