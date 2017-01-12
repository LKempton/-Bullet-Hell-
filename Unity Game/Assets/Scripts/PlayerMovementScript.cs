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

        private CameraRenderScript m_cr;
        private ChangeLayerScript m_cl;
        private PlayerDamageScript m_pd;
        

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            playerTransform = GetComponent<Transform>();

            // Find the renderinmg script located on the main camera.
            m_cr = GameObject.FindWithTag("MainCamera").GetComponent<CameraRenderScript>();
            m_cl = GameObject.FindWithTag("GameController").GetComponent<ChangeLayerScript>();
            m_pd = GameObject.FindWithTag("Player").GetComponent<PlayerDamageScript>();

            UpdateRenderLayers(currentLayer);
        }

        void Update()
        {
            if (Input.GetButtonDown("LayerUp") && currentLayer < 3)
            {
                currentLayer++;

                UpdateColour(currentLayer);

                playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 10, playerTransform.position.z);

                m_cl.SetLayerRecursively(gameObject, SelectLayer());
                UpdateRenderLayers(currentLayer);

                // Set an amount for shield for when the player changes layer.
                m_pd.SetShieldStatus(1);

            }

            else if (Input.GetButtonDown("LayerDown") && currentLayer > 1)
            {
                currentLayer--;

                UpdateColour(currentLayer);

                playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y - 10, playerTransform.position.z);

                m_cl.SetLayerRecursively(gameObject, SelectLayer());
                UpdateRenderLayers(currentLayer);

                // Set an amount for shield for when the player changes layer.
                m_pd.SetShieldStatus(1);
            }
 
        }

        void FixedUpdate()
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f , moveVertical);

            movement.Normalize();

            if (Input.GetButton("Boost"))
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

        // Calls the method in the CameraRenderScript that makes only objects on the same layer visible.
        void UpdateRenderLayers(int layer)
        {
            if (currentLayer == 1)
            {
                m_cr.ChangeCullingMask(10);
            }
            else if (currentLayer == 2)
            {
                m_cr.ChangeCullingMask(9);
            }
            else if (currentLayer == 3)
            {
                m_cr.ChangeCullingMask(8);
            }
        }

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