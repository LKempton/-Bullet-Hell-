using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PointsSystemScript : MonoBehaviour {

    private Text m_pointsText;
    private int m_points;

    void Start()
    {
        m_pointsText = gameObject.GetComponent<Text>();
    }

	public void PointsUp(int amount)
    {
        m_points += 10;
        m_pointsText.text = "Points " + m_points.ToString();
    }

    public int GetPoints()
    {
        return m_points;
    }
}
