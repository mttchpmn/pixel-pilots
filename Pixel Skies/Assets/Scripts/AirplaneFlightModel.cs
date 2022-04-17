using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO - Create a base class and then inherit for an arcade flight model and a sim flight model
public class AirplaneFlightModel : MonoBehaviour
{
    private const float MpsToKnotFactor = 1.94384f;
    
    private Rigidbody _rigidbody;
    private AirplaneInputBase _input;
    
    private float _initialDrag;
    private float _initialAngularDrag;
    
    private float _maxSpeedInMps;
    private float _normalizedSpeedInKnots;

    private float _angleOfAttack;
    private float _pitchAngle;
    private float _rollAngle;
    
    [Header("Speed Attributes")]
    public float forwardSpeed;
    public float speedInKnots;
    public float maxSpeedInKnots = 101f;
    
    [Header("Lift Attributes")]
    public float maxLiftForce = 800f;
    public AnimationCurve liftCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    [Header("Drag Attributes")] public float dragFactor = 0.01f;

    [Header("Controls Attributes")]
    public float pitchSensitivity = 10f;
    public float rollSensitivity = 10f;

    public void InitializeFlightModel(Rigidbody rb, AirplaneInputBase input)
    {
        _input = input;
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

        HandlePitch();
        HandleRoll();
        
        HandleRigidBodyTransform();
    }

    private void HandlePitch()
    {
        var flatForward = transform.forward;
        flatForward.y = 0f;
        flatForward = flatForward.normalized;

        _pitchAngle = Vector3.Angle(transform.forward, flatForward);
        Debug.Log($"Pitch angle: {_pitchAngle}");

        var pitchTorque = _input.Pitch * pitchSensitivity * transform.right;
        _rigidbody.AddTorque(pitchTorque);
    }

    private void HandleRoll()
    {
        var flatRight = transform.right;
        flatRight.y = 0;
        flatRight = flatRight.normalized;

        _rollAngle = Vector3.Angle(transform.right, flatRight);
        Debug.Log($"Roll angle: {_rollAngle}");

        var rollTorque = _input.Roll * rollSensitivity * transform.forward;
        _rigidbody.AddTorque(rollTorque * -1);
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