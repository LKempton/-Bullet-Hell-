using UnityEngine;
using System.Collections;

public class EnemyShootingScript : MonoBehaviour {

    public Transform[] spawners = new Transform[5];

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private float repeatRate = 2f;

    [SerializeField]
    private string shootingType = "instant";

    private bool canShoot = true;
    private bool isLooping = false;

    void Start()
    {
        InvokeRepeating("Shoot", 3f, repeatRate);
    }

    private void Shoot()
    {
        if (shootingType == "instant")
        {
            for (int i = 0; i < spawners.Length; i++)
            {
                Instantiate(bullet, spawners[i].position, spawners[i].rotation);
            }
        }
        else if (shootingType == "sequence" && canShoot == true)
        {
            StartCoroutine("SequenceShoot");

        }
        
    }

    private void ShootSequence()
    {

    }

    IEnumerator SequenceShoot()
    {
        canShoot = false;

        if (!isLooping)
        {
            for (int k = 0; k < spawners.Length; k++)
            {
                yield return new WaitForSeconds(0.2f);

                Instantiate(bullet, spawners[k].position, spawners[k].rotation);
            }
            isLooping = true;
        }

        else if (isLooping)
        {
            for (int k = spawners.Length - 1; k >= 0; k--)
            {
                yield return new WaitForSeconds(0.2f);

                Instantiate(bullet, spawners[k].position, spawners[k].rotation);
            }
            isLooping = false;
        }
       

        canShoot = true;
    }
}
