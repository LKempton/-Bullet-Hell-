using UnityEngine;
using System.Collections;

public class EnemySpawningScript : MonoBehaviour {

    [SerializeField]
    private GameObject[] enemies = new GameObject[3];

    [SerializeField]
    private Transform[] spawners = new Transform[3];

    [SerializeField]
    private int activeEnemies = 0;

    private GameControllerScript m_gc;
    private AlertScript m_alert;

    void Start()
    {
        m_gc = GameObject.FindWithTag("GameController").GetComponent<GameControllerScript>();
        m_alert = GameObject.FindWithTag("GameController").GetComponent<AlertScript>();

        InvokeRepeating("SpawnWave", 2, 6);
    }

    void SpawnWave()
    {
        int layer = Random.Range(1, 4);

        for (int i = 0; i < spawners.Length; i++)
        {
            float variation = Random.Range(-1f, 1f);

            Vector3 basePos = new Vector3(spawners[i].position.x + variation, spawners[i].position.y, spawners[i].position.z + variation);
            Vector3 middlePos = new Vector3(spawners[i].position.x + variation, spawners[i].position.y + 10, spawners[i].position.z + variation);
            Vector3 topPos = new Vector3(spawners[i].position.x + variation, spawners[i].position.y + 20, spawners[i].position.z + variation);

            if (m_gc.activeEnemies <= 9)
            {
                if (layer == 1)
                {
                    Instantiate(enemies[Random.Range(0, 3)], basePos, spawners[i].rotation);
                    m_gc.activeEnemies++;
                    m_alert.AddEnemies(10);
                }
                else if (layer == 2)
                {
                    Instantiate(enemies[Random.Range(0, 3)], middlePos, spawners[i].rotation);
                    m_gc.activeEnemies++;
                    m_alert.AddEnemies(9);
                }
                else if (layer == 3)
                {
                    Instantiate(enemies[Random.Range(0, 3)], topPos, spawners[i].rotation);
                    m_gc.activeEnemies++;
                    m_alert.AddEnemies(8);
                }

                layer = Random.Range(1, 4);
            }
            
        }
    }
}
