using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace LK
{
    [System.Serializable]
    public class Boundary
    {
        public float xMin, xMax, zMin, zMax;
    }

    public class PlayerMovementScript : MonoBehaviour
    {
        [SerializeField]
        private float speed = 5.0f;

        [SerializeField]
        private Boundary boundary;

        private int currentLayer = 1;

        private Rigidbody rb;
        private Transform playerTransform;

        [SerializeField]
        private GameObject borderTop;
        [SerializeField]
        private GameObject borderBottom;

        private Color bottomLayer = new Color(0.0f, 0.0f, 255f);
        private Color middleLayer = new Color(255f, 255f, 0.0f);
        private Color topLayer = new Color(255f, 0.0f, 0.0f);

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            playerTransform = GetComponent<Transform>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q) && currentLayer < 3)
            {
                currentLayer++;

                UpdateColour(currentLayer);

                Debug.Log(currentLayer);

                playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 10, playerTransform.position.z);
            }

            else if (Input.GetKeyDown(KeyCode.Q) && currentLayer == 3)
            {
                currentLayer = 1;

                UpdateColour(currentLayer);

                playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y - 20, playerTransform.position.z);
            }

            else if (Input.GetKeyDown(KeyCode.E) && currentLayer > 1)
            {
                currentLayer--;

                UpdateColour(currentLayer);

                playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y - 10, playerTransform.position.z);
            }

            else if (Input.GetKeyDown(KeyCode.E) && currentLayer == 1)
            {
                currentLayer = 3;

                UpdateColour(currentLayer);

                playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 20, playerTransform.position.z);
            }
        }

        void FixedUpdate()
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f , moveVertical);

            movement.Normalize();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                movement.z = movement.z * 1.5f;
            }
        
            rb.velocity = movement * speed;

            rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), playerTransform.position.y , Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));
        }

        void UpdateColour(int layer)
        {
            switch (layer)
            {
                case 1:
                    borderBottom.GetComponent<Image>().color = bottomLayer;
                    borderTop.GetComponent<Image>().color = bottomLayer;
                    break;
                case 2:
                    borderBottom.GetComponent<Image>().color = middleLayer;
                    borderTop.GetComponent<Image>().color = middleLayer;
                    break;
                case 3:
                    borderBottom.GetComponent<Image>().color = topLayer;
                    borderTop.GetComponent<Image>().color = topLayer;
                    break;
            }
        }
    }
}