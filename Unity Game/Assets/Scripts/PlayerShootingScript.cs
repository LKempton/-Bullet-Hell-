using UnityEngine;
using System.Collections;

namespace LK
{
    public class PlayerShootingScript : MonoBehaviour
    {
        [SerializeField]
        private Transform m_bulletSpawnerLeft;
        [SerializeField]
        private Transform m_bulletSpawnerRight;
        [SerializeField]
        private Transform m_rocketSpawner;
        [SerializeField]
        private GameObject m_bullet;
        private GameObject[] m_bulletArray = new GameObject[50];
        [SerializeField]
        private GameObject m_rocket;

        [SerializeField]
        private float m_bulletFireRate;
        [SerializeField]
        private float m_rocketFireRate;

        [SerializeField]
        private float m_shakeMagnitude;
        [SerializeField]
        private float m_shakeTime;

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
            for (int i = 0; i < m_bulletArray.Length; i++)
            {
                m_bulletArray[i] = Instantiate(m_bullet);
                m_bulletArray[i].SetActive(false);
            }
        }

        void Update()
        {
            if (Input.GetButton("Fire1") && Time.time > m_nextFireBullet)
            {
                m_nextFireBullet = Time.time + m_bulletFireRate;

                if (m_isFiringLeft == true)
                {
                    for (int i = 0; i < m_bulletArray.Length; i++)
                    {
                        if (!m_bulletArray[i].activeInHierarchy)
                        {
                            m_bulletArray[i].transform.position = m_bulletSpawnerLeft.transform.position;
                            m_bulletArray[i].transform.rotation = m_bulletSpawnerLeft.transform.rotation;
                            m_bulletArray[i].SetActive(true);

                            m_ss.PlaySoundClip(0);
                            break;
                        }
                    }
                    m_isFiringLeft = false;
                }
                else if (m_isFiringLeft == false)
                {
                    for (int i = 0; i < m_bulletArray.Length; i++)
                    {
                        if (!m_bulletArray[i].activeInHierarchy)
                        {
                            m_bulletArray[i].transform.position = m_bulletSpawnerRight.transform.position;
                            m_bulletArray[i].transform.rotation = m_bulletSpawnerRight.transform.rotation;
                            m_bulletArray[i].SetActive(true);

                            m_ss.PlaySoundClip(0);
                            break;
                        }
                    }

                    m_isFiringLeft = true;
                }

                m_camShake.Shake(m_shakeMagnitude, m_shakeTime);
            }

            if (Input.GetButton("Fire2") && Time.time > m_nextFireRocket)
            {
                m_nextFireRocket = Time.time + m_rocketFireRate;

                Instantiate(m_rocket, m_rocketSpawner.position, m_rocketSpawner.rotation);

                m_camShake.Shake(m_shakeMagnitude + 0.1f, m_shakeTime + 0.1f);

                m_ss.PlaySoundClip(1);
            }
        }

        public void DoubleShot()
        {
            m_bulletFireRate -= 0.1f;  

            StartCoroutine(PowerupTime());
        }

        IEnumerator PowerupTime()
        {
            yield return new WaitForSeconds(5);
            m_bulletFireRate += 0.1f;
        }

    }
}

