using UnityEngine;
using System.Collections;

namespace LK
{
    public class PlayerShootingScript : MonoBehaviour
    {
        [SerializeField]
        private Transform bulletSpawnerLeft;
        [SerializeField]
        private Transform bulletSpawnerRight;
        [SerializeField]
        private Transform rocketSpawner;
        [SerializeField]
        private GameObject bullet;
        private GameObject[] bulletArray = new GameObject[50];
        [SerializeField]
        private GameObject rocket;

        [SerializeField]
        private float bulletFireRate;
        [SerializeField]
        private float rocketFireRate;

        private bool isFiringLeft = true;

        private float nextFireBullet = 0.0f;

        private float nextFireRocket = 0.0f;

        void Start()
        {
            for (int i = 0; i < bulletArray.Length; i++)
            {
                bulletArray[i] = Instantiate(bullet);
                bulletArray[i].SetActive(false);
            }
        }

        void Update()
        {
            if (Input.GetMouseButton(0) && Time.time > nextFireBullet)
            {
                nextFireBullet = Time.time + bulletFireRate;

                if (isFiringLeft == true)
                {
                    for (int i = 0; i < bulletArray.Length; i++)
                    {
                        if (!bulletArray[i].activeInHierarchy)
                        {
                            bulletArray[i].transform.position = bulletSpawnerLeft.transform.position;
                            bulletArray[i].transform.rotation = bulletSpawnerLeft.transform.rotation;
                            bulletArray[i].SetActive(true);

                            break;
                        }
                    }
                   
                   

                    isFiringLeft = false;
                }
                else if (isFiringLeft == false)
                {
                    for (int i = 0; i < bulletArray.Length; i++)
                    {
                        if (!bulletArray[i].activeInHierarchy)
                        {
                            bulletArray[i].transform.position = bulletSpawnerRight.transform.position;
                            bulletArray[i].transform.rotation = bulletSpawnerRight.transform.rotation;
                            bulletArray[i].SetActive(true);

                            break;
                        }
                    }

                    isFiringLeft = true;
                }
            }

            if (Input.GetMouseButton(1) && Time.time > nextFireRocket)
            {
                nextFireRocket = Time.time + rocketFireRate;

                Instantiate(rocket, rocketSpawner.position, rocketSpawner.rotation);
            }
        }

    }
}

