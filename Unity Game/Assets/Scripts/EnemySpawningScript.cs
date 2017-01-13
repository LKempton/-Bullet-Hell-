using UnityEngine;
using System.Collections;

public class EnemySpawningScript : MonoBehaviour {

    [SerializeField]
    private GameObject[] enemies = new GameObject[4];

    private GameObject m_player;

    [SerializeField]
    private Transform[] spawners = new Transform[3];

    [SerializeField]
    private int activeEnemies = 0;

    private GameControllerScript m_gc;
    private AlertScript m_alert;
    private PointsSystemScript m_pointsSystem;

    void Start()
    {
        m_gc = GameObject.FindWithTag("GameController").GetComponent<GameControllerScript>();
        m_alert = GameObject.FindWithTag("GameController").GetComponent<AlertScript>();
        m_pointsSystem = GameObject.FindWithTag("PointsText").GetComponent<PointsSystemScript>();

        m_player = GameObject.FindWithTag("Player");

        InvokeRepeating("SpawnWave", 2, 6);
    }

    void Update()
    {
        // Waits until the player gets a multiple of 100 points.
        if (m_pointsSystem.GetPoints() % 100 == 0 && m_pointsSystem.GetPoints() != 0 && m_gc.eliteActive == false)
        {
            m_gc.eliteActive = true;
            SpawnElite();
        }
    }

    void SpawnWave()
    {
        // Spawns 3 enemies each wave on random spawners and layers.
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

    void SpawnElite()
    {
        // Spawns the elite enemy type at a random spawner on the same layer as the player.
        int _rndSpawner = Random.Range(1, 4);

        float variation = Random.Range(-1f, 1f);

        if (_rndSpawner == 1)
        {
            Vector3 _spawnPos = new Vector3(spawners[0].position.x + variation, m_player.transform.position.y, spawners[0].position.z + variation);
            Instantiate(enemies[3], _spawnPos, spawners[0].rotation);
        }
        else if (_rndSpawner == 2)
        {
            Vector3 _spawnPos = new Vector3(spawners[1].position.x + variation, m_player.transform.position.y, spawners[1].position.z + variation);
            Instantiate(enemies[3], _spawnPos, spawners[1].rotation);
        }
        else if (_rndSpawner == 3)
        {
            Vector3 _spawnPos = new Vector3(spawners[2].position.x + variation, m_player.transform.position.y, spawners[2].position.z + variation);
            Instantiate(enemies[3], _spawnPos, spawners[2].rotation);
        }
    }
}
