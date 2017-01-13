using UnityEngine;
using System.Collections;

public class LayerCheckScript : MonoBehaviour {

    [SerializeField]
    private GameObject[] m_sprite = new GameObject[3];

    private ChangeLayerScript m_cl;

    // Marks whether the enemy is part of the larger elite enemy.
    [SerializeField]
    private bool m_isElite;

    void Start()
    {
        m_cl = GameObject.FindWithTag("GameController").GetComponent<ChangeLayerScript>();

        CheckLayer();
    }

    // Finds the y position of the object and uses that to determine what layer it should be on.
    void CheckLayer()
    {
        if (m_isElite == false)
        {
            if (transform.position.y == 0)
            {
                m_cl.SetLayerRecursively(gameObject, 10);

                m_sprite[0].SetActive(true);
            }
            else if (transform.position.y == 10)
            {
                m_cl.SetLayerRecursively(gameObject, 9);

                m_sprite[1].SetActive(true);
            }
            else if (transform.position.y == 20)
            {
                m_cl.SetLayerRecursively(gameObject, 8);

                m_sprite[2].SetActive(true);
            }
        }
        
    }
}
