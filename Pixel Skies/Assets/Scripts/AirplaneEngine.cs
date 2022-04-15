using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneEngine : MonoBehaviour
{
    public float maxForce = 200f;
    public float maxRpm = 2550f;
    
    public AnimationCurve powerCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    
    public Vector3 CalculateForce(float throttleSetting)
    {
        var finalThrottle = Mathf.Clamp01(throttleSetting);
        finalThrottle = powerCurve.Evaluate(finalThrottle);
        var finalPower = finalThrottle * maxForce;
        var finalForce = transform.forward * finalPower;


        return finalForce;
    }
}
