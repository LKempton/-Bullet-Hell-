using UnityEngine;
using System.Collections;

public class CameraRenderScript : MonoBehaviour {

    private Camera m_thisCamera;

    void Awake()
    {
        m_thisCamera = gameObject.GetComponent<Camera>();
    }

    // Take the current player layer and then change the culling mask properties to switch on only that layer.
    public void ChangeCullingMask(int layer)
    {
        if (layer == 10)
        {
            // Switch on layer 10, leave others as-is.
            m_thisCamera.cullingMask |= (1 << 10);

            // Switch off layer 9 and 8, leave others as-is.
            m_thisCamera.cullingMask &= ~(1 << 9);
            m_thisCamera.cullingMask &= ~(1 << 8);
        }
        else if (layer == 9)
        {
            // Switch on layer 9, leave others as-is.
            m_thisCamera.cullingMask |= (1 << 9);

            // Switch off layer 10 and 8, leave others as-is.
            m_thisCamera.cullingMask &= ~(1 << 10);
            m_thisCamera.cullingMask &= ~(1 << 8);
        }
        else if (layer == 8)
        {
            // Switch on layer 8, leave others as-is.
            m_thisCamera.cullingMask |= (1 << 8);

            // Switch off layer 10 and 9, leave others as-is.
            m_thisCamera.cullingMask &= ~(1 << 9);
            m_thisCamera.cullingMask &= ~(1 << 10);
        }
    }
}
