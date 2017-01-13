using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlertScript : MonoBehaviour {

    private int[] m_enemies = new int[3];

    [SerializeField]
    private Text m_alertMessage;

    private bool m_isTextLimited = false;

    private SoundScript m_sm;

    void Start()
    {
        m_sm = GameObject.FindWithTag("SoundManager").GetComponent<SoundScript>();
    }

    void Update()
    {
        if (m_enemies[0] >= 3 && m_isTextLimited == false)
        {
            m_isTextLimited = true;

            StopCoroutine(ShowAlert(-1));

            m_alertMessage.text = "";

            StartCoroutine(ShowAlert(1));
        }

        if (m_enemies[1] >= 3 && m_isTextLimited == false)
        {
            m_isTextLimited = true;

            StopCoroutine(ShowAlert(-1));

            m_alertMessage.text = "";

            StartCoroutine(ShowAlert(2));
        }

        if (m_enemies[2] >= 3 && m_isTextLimited == false)
        {
            m_isTextLimited = true;

            StopCoroutine(ShowAlert(-1));

            m_alertMessage.text = "";

            StartCoroutine(ShowAlert(3));
        }

        if (m_enemies[0] < 3 && m_enemies[1] < 3 && m_enemies[2] < 3)
        {
            m_isTextLimited = false;
        }
    }

    public void AddEnemies(int layer)
    {
        if (layer == 10)
        {
            m_enemies[0]++;
        }
        else if (layer == 9)
        {
            m_enemies[1]++;
        }
        else if (layer == 8)
        {
            m_enemies[2]++;
        }
    }

    public void RemoveEnemies(int layer)
    {
        if (layer == 10)
        {
            m_enemies[0]--;
        }
        else if (layer == 9)
        {
            m_enemies[1]--;
        }
        else if (layer == 8)
        {
            m_enemies[2]--;
        }
    }

    IEnumerator ShowAlert(int layer)
    {
        if (layer == 1)
        {
            m_alertMessage.text = "Warning: Hostile Activity at Ground Level!";

            yield return new WaitForSeconds(2);

            m_alertMessage.text = "";
        }
        else if (layer == 2)
        {
            m_alertMessage.text = "Warning: Hostile Activity at Sky Level!";

            yield return new WaitForSeconds(2);

            m_alertMessage.text = "";
        }
        else if (layer == 3)
        {
            m_alertMessage.text = "Warning: Hostile Activity at Space Level!";

            yield return new WaitForSeconds(2);

            m_alertMessage.text = "";
        }
    }
}
