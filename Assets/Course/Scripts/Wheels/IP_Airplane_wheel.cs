using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WheelCollider))]
public class IP_Airplane_wheel : MonoBehaviour
{
    private WheelCollider _wheelCollider;
    // Start is called before the first frame update
    void Start()
    {
        _wheelCollider = GetComponent<WheelCollider>();
    }
    
    public void InitWheel()
    {
        if (_wheelCollider != null)
            _wheelCollider.motorTorque = 0.00000000000000000001f;
    }
}
