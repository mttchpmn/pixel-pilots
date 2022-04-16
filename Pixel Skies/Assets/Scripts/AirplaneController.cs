using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AirplaneFlightModel))]
public class AirplaneController : RigidbodyControllerBase
{
    [Header("Base Airplane Properties")]
    public AirplaneInputBase input;
    public AirplaneFlightModel flightModel;
    public Transform centerOfGravityPosition;

    [Tooltip("Weight in kilograms")] public float airplaneWeight = 400f;

    [Header("Engines")] public List<AirplaneEngine> engines = new List<AirplaneEngine>();
    [Header("Wheels")] public List<AirplaneWheel> wheels = new List<AirplaneWheel>();

    public override void Start()
    {
        base.Start();

        if (_rigidbody == null) return;

        _rigidbody.mass = airplaneWeight;

        if (centerOfGravityPosition != null)
        {
            _rigidbody.centerOfMass = centerOfGravityPosition.localPosition;
        }

        if (wheels.Any())
        {
            foreach (var wheel in wheels)
            {
                wheel.InitWheel();
            }
        }

        flightModel = GetComponent<AirplaneFlightModel>();
        if (flightModel != null)
            flightModel.InitializeFlightModel(_rigidbody, input);
    }

    protected override void HandlePhysics()
    {
        if (input == null)
            return;

        HandleEngines();
        HandleFlightModel();
        HandleSteering();
        HandleBrakes();
        HandleAltitude();
    }

    private void HandleEngines()
    {
        if (!engines.Any()) return;

        foreach (var engine in engines)
        {
            var thrust = engine.CalculateForce(input.ThrottleSetting);
            _rigidbody.AddForce(thrust);
        }
    }

    private void HandleFlightModel()
    {
        if (flightModel != null)
            flightModel.UpdateFlightModel();
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