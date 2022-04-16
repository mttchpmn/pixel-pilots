using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneFlightModel : MonoBehaviour
{
    private const float MpsToKnotFactor = 1.94384f;
    
    private Rigidbody _rigidbody;
    private float _initialDrag;
    private float _initialAngularDrag;
    
    [Header("Flight Attributes")]
    public float forwardSpeed;
    public float speedInKnots;

    public void InitializeFlightModel(Rigidbody rb)
    {
        _rigidbody = rb;
        _initialDrag = rb.drag;
        _initialAngularDrag = rb.angularDrag;
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
        forwardSpeed = localVelocity.z;
        speedInKnots = forwardSpeed * MpsToKnotFactor;
    }

    private void CalculateLift()
    {
    }

    private void CalculateDrag()
    {
    }
}