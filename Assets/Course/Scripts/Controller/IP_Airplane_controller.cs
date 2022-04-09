using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IP_Airplane_controller : IP_BaseRigidbody_controller
{
    [Header("Base Airplane Properties")]
    public IP_BaseAirplane_Input input;

    public Transform centerOfGravitiyPosition;

    [Tooltip("Weight in kilograms")]
    public float airplaneWeight = 400f;
    
    protected override void HandlePhysics()
    {
        HandleEngines();
        HandleAerodynamics();
        HandleSteering();
        HandleBrakes();
        HandleAltitude();
    }

    private void HandleEngines()
    {
        throw new System.NotImplementedException();
    }
    
    private void HandleAerodynamics()
    {
        throw new System.NotImplementedException();
    }
    
    private void HandleSteering()
    {
        throw new System.NotImplementedException();
    }
    private void HandleBrakes()
    {
        throw new System.NotImplementedException();
    }

    private void HandleAltitude()
    {
        throw new System.NotImplementedException();
    }
}
