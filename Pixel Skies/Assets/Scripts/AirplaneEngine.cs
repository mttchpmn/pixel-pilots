using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneEngine : MonoBehaviour
{
    [Header("Engine Properties")] public float maxForce = 200f;
    public float maxRpm = 2550f;
    public AnimationCurve powerCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    [Header("Propellers")] public AirplanePropeller propeller;

    public Vector3 CalculateForce(float throttleSetting)
    {
        var finalThrottle = Mathf.Clamp01(throttleSetting);
        finalThrottle = powerCurve.Evaluate(finalThrottle);

        var currentRpm = finalThrottle * maxRpm;
        if (propeller != null)
            propeller.HandlePropeller(currentRpm);
        
        var finalPower = finalThrottle * maxForce;
        var finalForce = transform.forward * finalPower;


        return finalForce;
    }
}