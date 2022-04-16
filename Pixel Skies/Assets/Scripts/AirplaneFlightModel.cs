using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneFlightModel : MonoBehaviour
{
    private const float MpsToKnotFactor = 1.94384f;
    
    private Rigidbody _rigidbody;
    private float _initialDrag;
    private float _initialAngularDrag;
    private float _maxSpeedInMps;
    private float _normalizedSpeedInKnots;
    
    [Header("Speed Attributes")]
    public float forwardSpeed;
    public float speedInKnots;
    public float maxSpeedInKnots = 101f;
    
    [Header("Lift Attributes")]
    public float maxLiftForce = 800f;
    public AnimationCurve liftCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    public void InitializeFlightModel(Rigidbody rb)
    {
        _rigidbody = rb;
        _initialDrag = rb.drag;
        _initialAngularDrag = rb.angularDrag;

        _maxSpeedInMps = maxSpeedInKnots / MpsToKnotFactor;
    }

    public void UpdateFlightModel()
    {
        if (_rigidbody == null) return;
        
        CalculateForwardSpeed();
        CalculateLift();
        CalculateDrag();
    }

    private void CalculateForwardSpeed()
    {
        var localVelocity = transform.InverseTransformDirection(_rigidbody.velocity);
        forwardSpeed = Mathf.Max(0f, localVelocity.z);
        forwardSpeed = Mathf.Clamp(forwardSpeed, 0f, _maxSpeedInMps);
        
        speedInKnots = forwardSpeed * MpsToKnotFactor;
        _normalizedSpeedInKnots = Mathf.InverseLerp(0f, maxSpeedInKnots, speedInKnots);
    }

    private void CalculateLift()
    {
        var liftDirection = transform.up;
        var liftForce = liftCurve.Evaluate(_normalizedSpeedInKnots) * maxLiftForce;
        
        var finalLiftForce = liftDirection * liftForce;
        _rigidbody.AddForce(finalLiftForce);
    }

    private void CalculateDrag()
    {
    }
}