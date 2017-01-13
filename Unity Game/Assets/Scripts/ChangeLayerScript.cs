using UnityEngine;
using System.Collections;

public class ChangeLayerScript : MonoBehaviour {

    // Go through each child object recursively and set them to the same layer as the parent.
    public void SetLayerRecursively(GameObject obj, int newLayer)
    {
        // Goes through each child object recursively and sets the layer to the same as the parent object.
        if (obj == null)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }

            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
