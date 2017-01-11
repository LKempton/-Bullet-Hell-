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

        private bool m_isFiringLeft = true;

        private float m_nextFireBullet = 0.0f;

        private float m_nextFireRocket = 0.0f;

        private bool m_isDoubleShot = false;

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
            if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && Time.time > m_nextFireBullet)
            {
                m_nextFireBullet = Time.time + bulletFireRate;

                if (m_isFiringLeft == true)
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
                    m_isFiringLeft = false;
                }
                else if (m_isFiringLeft == false)
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

                    m_isFiringLeft = true;
                }
            }

            if (Input.GetMouseButton(1) && Time.time > m_nextFireRocket)
            {
                m_nextFireRocket = Time.time + rocketFireRate;

                Instantiate(rocket, rocketSpawner.position, rocketSpawner.rotation);
            }
        }

        public void DoubleShot()
        {
            bulletFireRate -= 0.1f;  

            StartCoroutine(PowerupTime());
        }

        IEnumerator PowerupTime()
        {
            yield return new WaitForSeconds(5);
            bulletFireRate += 0.1f;
        }

    }
}

