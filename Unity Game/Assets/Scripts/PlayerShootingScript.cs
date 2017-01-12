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

        [SerializeField]
        private float shakeMagnitude;
        [SerializeField]
        private float shakeTime;

        private bool m_isFiringLeft = true;
        private float m_nextFireBullet = 0.0f;
        private float m_nextFireRocket = 0.0f;
        private bool m_isDoubleShot = false;

        private SoundScript m_ss;
        private CameraShaker m_camShake;

        void Start()
        {
            m_ss = GameObject.FindWithTag("SoundManager").GetComponent<SoundScript>();
            m_camShake = GameObject.FindWithTag("ShakeableCamera").GetComponent<CameraShaker>();
            for (int i = 0; i < bulletArray.Length; i++)
            {
                bulletArray[i] = Instantiate(bullet);
                bulletArray[i].SetActive(false);
            }
        }

        void Update()
        {
            if (Input.GetButton("Fire1") && Time.time > m_nextFireBullet)
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

                            m_ss.PlaySoundClip(0);
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

                            m_ss.PlaySoundClip(0);
                            break;
                        }
                    }

                    m_isFiringLeft = true;
                }

                m_camShake.Shake(shakeMagnitude, shakeTime);
            }

            if (Input.GetButton("Fire2") && Time.time > m_nextFireRocket)
            {
                m_nextFireRocket = Time.time + rocketFireRate;

                Instantiate(rocket, rocketSpawner.position, rocketSpawner.rotation);

                m_camShake.Shake(shakeMagnitude + 0.1f, shakeTime + 0.1f);

                m_ss.PlaySoundClip(1);
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

