using UnityEngine;
using System.Collections;

public class LayerCheckScript : MonoBehaviour {

    [SerializeField]
    private GameObject[] m_sprite = new GameObject[3];

    private ChangeLayerScript m_cl;

    void Start()
    {
        m_cl = GameObject.FindWithTag("GameController").GetComponent<ChangeLayerScript>();

        CheckLayer();
    }

    void CheckLayer()
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
