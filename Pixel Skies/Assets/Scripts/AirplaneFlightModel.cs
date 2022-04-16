using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO - Create a base class and then inherit for an arcade flight model and a sim flight model
public class AirplaneFlightModel : MonoBehaviour
{
    private const float MpsToKnotFactor = 1.94384f;
    
    private Rigidbody _rigidbody;
    
    private float _initialDrag;
    private float _initialAngularDrag;
    
    private float _maxSpeedInMps;
    private float _normalizedSpeedInKnots;

    private float _angleOfAttack;
    
    [Header("Speed Attributes")]
    public float forwardSpeed;
    public float speedInKnots;
    public float maxSpeedInKnots = 101f;
    
    [Header("Lift Attributes")]
    public float maxLiftForce = 800f;
    public AnimationCurve liftCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    [Header("Drag Attributes")] public float dragFactor = 0.01f;

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
        HandleRigidBodyTransform();
    }

    private void HandleRigidBodyTransform()
    {
        if (!(_rigidbody.velocity.magnitude > 1)) return;
        
        var updatedVelocity = Vector3.Lerp(_rigidbody.velocity, transform.forward * forwardSpeed, forwardSpeed * _angleOfAttack * Time.deltaTime);
        _rigidbody.velocity = updatedVelocity;

        var updatedRotation = Quaternion.Slerp(_rigidbody.rotation,
            Quaternion.LookRotation(_rigidbody.velocity.normalized, transform.up), Time.deltaTime);
        _rigidbody.MoveRotation(updatedRotation);
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
        _angleOfAttack = Vector3.Dot(_rigidbody.velocity.normalized, transform.forward);
        _angleOfAttack *= _angleOfAttack;
        
        Debug.Log($"AoA: {_angleOfAttack}");

        var liftDirection = transform.up;
        var liftForce = liftCurve.Evaluate(_normalizedSpeedInKnots) * maxLiftForce;
        
        var finalLiftForce = liftDirection * liftForce * _angleOfAttack;
        _rigidbody.AddForce(finalLiftForce);
    }

    private void CalculateDrag()
    {
        var parasiteDrag = forwardSpeed * dragFactor;
        var totalDrag = _initialDrag + parasiteDrag;

        _rigidbody.drag = totalDrag;
        _rigidbody.angularDrag = _initialAngularDrag * forwardSpeed;
    }
}