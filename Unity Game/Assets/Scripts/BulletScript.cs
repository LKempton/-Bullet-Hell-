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

        private ChangeLayerScript m_cl;

        void OnEnable()
        {
            // When the bullet/rocket is enabled give it a velocity at the speed put in the forward direction.
            bulletRB = GetComponent<Rigidbody>();

            m_cl = GameObject.FindWithTag("GameController").GetComponent<ChangeLayerScript>();

            if (!isRocket)
            {
                m_cl.SetLayerRecursively(gameObject, SelectLayer());
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

        // Find the layer the bullet should be on based on a range of values.
        private int SelectLayer()
        {
            if (transform.position.y > -0.5 && transform.position.y < 0.5)
            {
                return 10;
            }
            else if (transform.position.y > -10.5 && transform.position.y < 10.5)
            {
                return 9;
            }
            else if (transform.position.y > -20.5 && transform.position.y < 20.5)
            {
                return 8;
            }
            return 10;
        }

    }
}

