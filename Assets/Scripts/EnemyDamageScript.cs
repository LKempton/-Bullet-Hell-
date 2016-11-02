using UnityEngine;
using System.Collections;

public class EnemyDamageScript : MonoBehaviour {

    [SerializeField]
    private float health = 5f;

    private GameControllerScript gc;

    void Start()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<GameControllerScript>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Bullet"))
        {
            health -= 1;
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
}
