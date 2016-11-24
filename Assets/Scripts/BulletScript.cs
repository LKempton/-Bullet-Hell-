using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace LK
{
    public class BulletScript : MonoBehaviour
    {
        private Rigidbody bulletRB;

        [SerializeField]
        private float speed = 5.0f;

        [SerializeField]
        private float activeTime = 5.0f;

        [SerializeField]
        private bool isRocket = false;

        // Variable to store when the bullet was created.
        private float timeCreated;

        private Color bottomLayer = new Color(0.0f, 0.0f, 255f);
        private Color middleLayer = new Color(255f, 255f, 0.0f);
        private Color topLayer = new Color(255f, 0.0f, 0.0f);

        void Strt()
        {
            
        }

        void OnEnable()
        {
            bulletRB = GetComponent<Rigidbody>();

            if (!isRocket)
            {
                SelectColour();
                bulletRB.velocity = transform.right * speed;
            }
            else if (isRocket)
            {
                bulletRB.velocity = transform.up * speed;
            }
            // Set the time created to the current time when the script is started.
            timeCreated = Time.time;
        }

       
        void Update()
        {
            // If the bullet has been active lonnger than the activeTime allows destroy it.
            if (activeTime < Time.time - timeCreated)
            {
                gameObject.SetActive(false);
            }
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
}

