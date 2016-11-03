using UnityEngine;
using System.Collections;

public class EnemyDamageScript : MonoBehaviour {

    [SerializeField]
    private float health = 5f;

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
        if (health <= 0)
        {
            StopAllCoroutines();

            gc.activeEnemies--;

            Destroy(gameObject);
        }
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
