using UnityEngine;
using System.Collections;

public class LayerCheckScript : MonoBehaviour {

    [SerializeField]
    private GameObject[] m_sprite = new GameObject[3];

    void Start()
    {
        CheckLayer();
    }

    void CheckLayer()
    {
        if (transform.position.y == 0)
        {
            m_sprite[0].SetActive(true);
        }
        else if (transform.position.y == 10)
        {
            m_sprite[1].SetActive(true);
        }
        else if (transform.position.y == 20)
        {
            m_sprite[2].SetActive(true);
        }
    }
}
