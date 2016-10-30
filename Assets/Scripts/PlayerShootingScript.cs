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
        [SerializeField]
        private GameObject rocket;

        [SerializeField]
        private float bulletFireRate;
        [SerializeField]
        private float rocketFireRate;

        private bool isFiringLeft = true;

        private float nextFireBullet = 0.0f;

        private float nextFireRocket = 0.0f;

        void Update()
        {
            if (Input.GetMouseButton(0) && Time.time > nextFireBullet)
            {
                nextFireBullet = Time.time + bulletFireRate;

                if (isFiringLeft == true)
                {
                    Instantiate(bullet, bulletSpawnerLeft.position, bulletSpawnerLeft.rotation);

                    isFiringLeft = false;
                }
                else if (isFiringLeft == false)
                {
                    Instantiate(bullet, bulletSpawnerRight.position, bulletSpawnerRight.rotation);

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

