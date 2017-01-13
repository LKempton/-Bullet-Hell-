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

        // Contantly repeats the shooting script every 'repeatRate' seconds after 3 seconds of spawning.
        InvokeRepeating("Shoot", 3f, repeatRate);
    }

    private void Shoot()
    {
        // Used for the two enemies that spawn bullets instantly, goes through all the spawners and makes bullets.
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
        // For the slower firing enemy, shoots bullets from each spawner every 0.2 seconds.
        else if (shootingType == "sequence" && m_canShoot == true)
        {
            StartCoroutine("SequenceShoot");
        }
    }

    IEnumerator SequenceShoot()
    {
        m_canShoot = false;

        // Makes sure the bullets fire from opposite sides each time a wave ends.
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
