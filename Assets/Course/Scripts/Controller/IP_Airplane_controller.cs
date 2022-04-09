using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IP_Airplane_controller : IP_BaseRigidbody_controller
{
    [Header("Base Airplane Properties")] public IP_BaseAirplane_Input input;

    public Transform centerOfGravitiyPosition;

    [Tooltip("Weight in kilograms")] public float airplaneWeight = 400f;

    [Header("Engines")] public List<IP_Airplane_engine> engines = new List<IP_Airplane_engine>();
    [Header("Wheels")] public List<IP_Airplane_wheel> wheels = new List<IP_Airplane_wheel>();

    public override void Start()
    {
        base.Start();

        if (_rigidbody == null) return;

        _rigidbody.mass = airplaneWeight;

        if (centerOfGravitiyPosition != null)
        {
            _rigidbody.centerOfMass = centerOfGravitiyPosition.localPosition;
        }

        if (wheels.Any())
        {
            foreach (var wheel in wheels)
            {
                wheel.InitWheel();
            }
        }
    }

    protected override void HandlePhysics()
    {
        if (input == null)
            return;

        HandleEngines();
        HandleAerodynamics();
        HandleSteering();
        HandleBrakes();
        HandleAltitude();
    }

    private void HandleEngines()
    {
        if (!engines.Any()) return;

        foreach (var engine in engines)
        {
            var thrust = engine.CalculateForce(input.Throttle);
            _rigidbody.AddForce(thrust);
        }
    }

    private void HandleAerodynamics()
    {
    }

    private void HandleSteering()
    {
    }

    private void HandleBrakes()
    {
    }

    private void HandleAltitude()
    {
    }
}