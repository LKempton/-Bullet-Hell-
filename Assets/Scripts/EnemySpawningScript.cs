using UnityEngine;
using System.Collections;

public class EnemySpawningScript : MonoBehaviour {

    [SerializeField]
    private GameObject[] enemies = new GameObject[3];

    [SerializeField]
    private Transform[] spawners = new Transform[3];

    [SerializeField]
    private int activeEnemies = 0;

    [SerializeField]
    private bool[,] layerArray = new bool[3, 3];

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int k = 0; k < 3; k++)
            {
                layerArray[i, k] = false;
            }
        }
        InvokeRepeating("SpawnWave", 2, 10);
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

            if (activeEnemies <= 9)
            {
                if (layer == 1 && layerArray[i , 0] == false)
                {
                    Instantiate(enemies[Random.Range(0, 3)], basePos, spawners[i].rotation);
                    activeEnemies++;

                    layerArray[i, 0] = true;
                }
                else if (layer == 2 && layerArray[i, 1] == false)
                {
                    Instantiate(enemies[Random.Range(0, 3)], middlePos, spawners[i].rotation);
                    activeEnemies++;

                    layerArray[i, 1] = true;
                }
                else if (layer == 3 && layerArray[i, 2] == false)
                {
                    Instantiate(enemies[Random.Range(0, 3)], topPos, spawners[i].rotation);
                    activeEnemies++;

                    layerArray[i, 2] = true;
                }

                

                layer = Random.Range(1, 3);
            }
            
        }
    }
}
