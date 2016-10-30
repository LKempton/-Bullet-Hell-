using UnityEngine;
using System.Collections;

public class EnemyShootingScript : MonoBehaviour {

    public Transform[] spawners = new Transform[5];

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private float repeatRate = 2f;

    void Start()
    {
        InvokeRepeating("Shoot", 3f, repeatRate);
    }

    private void Shoot()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            Instantiate(bullet, spawners[i].position, spawners[i].rotation);
        }
    }
}
