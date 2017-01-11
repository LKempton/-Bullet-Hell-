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

    private bool canShoot = true;
    private bool isLooping = false;

    private Color bottomLayer = new Color(0.0f, 0.0f, 255f);
    private Color middleLayer = new Color(255f, 255f, 0.0f);
    private Color topLayer = new Color(255f, 0.0f, 0.0f);

    void Start()
    {
        SelectColour();

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
        }
        else if (shootingType == "sequence" && canShoot == true)
        {
            StartCoroutine("SequenceShoot");
        }
    }

    IEnumerator SequenceShoot()
    {
        canShoot = false;

        if (!isLooping)
        {
            for (int k = 0; k < spawners.Length; k++)
            {
                yield return new WaitForSeconds(0.2f);

                Instantiate(bullet, spawners[k].position, spawners[k].rotation);
            }
            isLooping = true;
        }

        else if (isLooping)
        {
            for (int k = spawners.Length - 1; k >= 0; k--)
            {
                yield return new WaitForSeconds(0.2f);

                Instantiate(bullet, spawners[k].position, spawners[k].rotation);
            }
            isLooping = false;
        }
       

        canShoot = true;
    }

    void SelectColour()
    {
        if (transform.position.y == 0)
        {
            gameObject.GetComponent<Renderer>().material.color = bottomLayer;
        }
        else if (transform.position.y == 10)
        {
            gameObject.GetComponent<Renderer>().material.color = middleLayer;
        }
        else if (transform.position.y == 20)
        {
            gameObject.GetComponent<Renderer>().material.color = topLayer;
        }
    }
}
