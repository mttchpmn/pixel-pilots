using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private float _throttleSetting = 0f;
    
    [SerializeField] private float _thrustFactor = 10;
    [SerializeField] private float _throttleFactor = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleThrottleChange();
        // TODO - The plane should accelerate based on throttle. Current velocity / inertia needs to be taken into account
        // Throttle should not control force directly
        ApplyThrust();
        // Debug.Log($"Velocity: {_rigidBody.velocity}");
        Debug.Log($"Throttle Setting: {_throttleSetting}");
        
        
        var inertia = _rigidBody.inertiaTensor;
        Debug.Log($"Inertia: {inertia}");
    }

    private void HandleThrottleChange()
    {
        if (Input.GetKey(KeyCode.W))
            IncreaseThrottle();
        
        if (Input.GetKey(KeyCode.S))
            DecreaseThrottle();
    }

    private void DecreaseThrottle()
    {
        var throttleCandidate = _throttleSetting -= _throttleFactor;

        _throttleSetting =  throttleCandidate > 0 ? throttleCandidate : 0f;
    }

    private void IncreaseThrottle()
    {
        var throttleCandidate = _throttleSetting += _throttleFactor;

        _throttleSetting =  throttleCandidate < 1 ? throttleCandidate : 1f;
    }
    
    private void ApplyThrust()
    {
        var thrust = _throttleSetting * _thrustFactor;
        Debug.Log($"Applying thrust of {thrust}");
        
        _rigidBody.AddRelativeForce(Vector3.forward * thrust);
    }

}