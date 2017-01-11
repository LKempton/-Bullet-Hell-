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

    private bool m_canShoot = true;
    private bool m_isLooping = false;

    private GameObject m_player;
    private SoundScript m_ss;

    void Start()
    {
        m_player = GameObject.FindWithTag("Player");
        m_ss = GameObject.FindWithTag("SoundManager").GetComponent<SoundScript>();

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
            if (gameObject.layer == m_player.layer)
            {
                m_ss.PlaySoundClip(3);
            }
        }
        else if (shootingType == "sequence" && m_canShoot == true)
        {
            StartCoroutine("SequenceShoot");
        }
    }

    IEnumerator SequenceShoot()
    {
        m_canShoot = false;

        if (!m_isLooping)
        {
            for (int k = 0; k < spawners.Length; k++)
            {
                yield return new WaitForSeconds(0.2f);

                Instantiate(bullet, spawners[k].position, spawners[k].rotation);

                if (gameObject.layer == m_player.layer)
                {
                    m_ss.PlaySoundClip(3);
                }
            }
            m_isLooping = true;
        }

        else if (m_isLooping)
        {
            for (int k = spawners.Length - 1; k >= 0; k--)
            {
                yield return new WaitForSeconds(0.2f);

                Instantiate(bullet, spawners[k].position, spawners[k].rotation);

                if (gameObject.layer == m_player.layer)
                {
                    m_ss.PlaySoundClip(3);
                }
            }
            m_isLooping = false;
        }
       

        m_canShoot = true;
    }
}
