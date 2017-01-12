using UnityEngine;
using System.Collections;


public class BGScroller : MonoBehaviour {

    [SerializeField]
    private float m_scrollSpeed;

    [SerializeField]
    private bool m_isVerticalScroll = false;

    [SerializeField]
    private bool m_isForwards = false;

    Renderer bgRenderer;

	// Use this for initialization
	void Start ()
    {
        bgRenderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_isVerticalScroll == true && m_isForwards == false)
        {
            Vector2 offset = new Vector2(0, Time.time * m_scrollSpeed);
            bgRenderer.material.mainTextureOffset = offset;
        }
        else if (m_isVerticalScroll == false && m_isForwards == false)
        {
            Vector2 offset = new Vector2(Time.time * m_scrollSpeed, 0);
            bgRenderer.material.mainTextureOffset = offset;
        }
        else if (m_isVerticalScroll == true && m_isForwards == true)
        {
            Vector2 offset = new Vector2(Time.time * -m_scrollSpeed, 0);
            bgRenderer.material.mainTextureOffset = offset;
        }
        else if (m_isVerticalScroll == false && m_isForwards == true)
        {
            Vector2 offset = new Vector2(Time.time * -m_scrollSpeed, 0);
            bgRenderer.material.mainTextureOffset = offset;
        }
       
	}
}
