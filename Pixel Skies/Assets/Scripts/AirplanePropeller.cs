using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplanePropeller : MonoBehaviour
{
    public void HandlePropeller(float currentRpm)
    {
        var degreesPerSecond = currentRpm * 360f / 60f * Time.deltaTime;
        
        transform.Rotate(Vector3.forward, degreesPerSecond);
    }
}
