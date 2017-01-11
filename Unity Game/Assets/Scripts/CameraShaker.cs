using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraShaker : MonoBehaviour {

    private static List<CameraShaker> shakers = new List<CameraShaker>();

    float maxMagnitude = 0.0f;
    float timeLeft_s = 0.0f;
    float timeTotal_s = 0.0f;

    // Activate every CameraShaker in the scene.
    //
    // time_s: Time (in seconds) for the shake to take.
    // magnitude: distance (in game units) for the shake to move the camera at its most extreme.
    //
    public void ShakeAllCameras(float time_s, float magnitude)
    {
        foreach (CameraShaker s in shakers)
        {
            s.Shake(time_s, magnitude);
        }
    }

    void Start()
    {

        // Add it to the global list of camera shakers.
        shakers.Add(this);
    }

    void Update()
    {

        // Subtract time until it reaches zero, then the shake is done.  Reset the system.
        timeLeft_s -= Time.deltaTime;
        if (timeLeft_s <= 0.0f)
        {
            timeLeft_s = 0.0f;
            timeTotal_s = 0.0f;
        }

        // Set the local position to the shaken offset.
        transform.localPosition = GetOffset();
    }

    // Generates a random camera offset based on the current state of the shake.
    //
    Vector2 GetOffset()
    {

        // Default offset is zero -- no shake.
        Vector2 offset = Vector2.zero;

        // If there is a current shake in play.
        if (timeLeft_s > 0f && timeTotal_s > 0f)
        {

            // Choose a random direction.
            float direction = Random.Range(0.0f, 360.0f);

            // Create a vector (relative to camera orientation) with a random direction rotated around the forward axis.
            offset = Quaternion.AngleAxis(direction, transform.forward) * transform.right;

            // Determine the current effective spread (between 0 and maxSpread) and multiply the offset by that.
            offset *= Mathf.Lerp(0.0f, maxMagnitude, timeLeft_s / timeTotal_s);
        }

        return offset;
    }

    // Begin a new shake.
    //
    // time_s: Time (in seconds) for the shake to take.
    // magnitude: distance (in game units) for the shake to move the camera at its most extreme.
    //
    public void Shake(float time_s, float magnitude)
    {
        timeTotal_s = time_s;
        timeLeft_s = time_s;
        maxMagnitude = magnitude;
    }
}
