using UnityEngine;
using System.Collections;

public class EliteEnemyBehaviour : MonoBehaviour {

    [SerializeField]
    private GameObject[] m_shooters = new GameObject[2];

    private GameObject m_player;
    private ChangeLayerScript m_changeLayer;

    void Start()
    {
        m_player = GameObject.FindWithTag("Player");
        m_changeLayer = GameObject.FindWithTag("GameController").GetComponent<ChangeLayerScript>();

        m_changeLayer.SetLayerRecursively(gameObject, m_player.layer);

        InvokeRepeating("Attack", 2, 6);
    }

    void Attack()
    {
        int rng = Random.Range(1, 4);

        if (rng == 1)
        {
            m_shooters[0].SetActive(true);

            StartCoroutine(WaveTime(m_shooters[0]));
        }
        else if (rng == 2)
        {
            m_shooters[1].SetActive(true);

            StartCoroutine(WaveTime(m_shooters[1]));
        }
    }

    IEnumerator WaveTime(GameObject obj)
    {
        yield return new WaitForSeconds(5);

        obj.SetActive(false);
    }
}
