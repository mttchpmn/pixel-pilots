using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AirplaneController : RigidbodyControllerBase
{
    [Header("Base Airplane Properties")] public AirplaneInputBase input;

    public Transform centerOfGravitiyPosition;

    [Tooltip("Weight in kilograms")] public float airplaneWeight = 400f;

    [Header("Engines")] public List<AirplaneEngine> engines = new List<AirplaneEngine>();
    [Header("Wheels")] public List<AirplaneWheel> wheels = new List<AirplaneWheel>();

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