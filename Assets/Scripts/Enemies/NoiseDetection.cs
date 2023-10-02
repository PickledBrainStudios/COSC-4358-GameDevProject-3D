using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseDetection : MonoBehaviour
{
    public float noiseDetectionRadius = 5f;

    void DetectNoise(Vector3 noisePosition)
    {
        // Check if the noise is within the detection radius
        if (Vector3.Distance(transform.position, noisePosition) <= noiseDetectionRadius)
        {
            // Investigate the noise
            // Implement your investigation logic here
        }
    }
}
