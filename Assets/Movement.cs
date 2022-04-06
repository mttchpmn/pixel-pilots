using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody _rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKey(KeyCode.LeftArrow))
       {
           Debug.Log("Pressed left");
           _rigidBody.AddRelativeForce(Vector3.left * 5f);
       } 
       if (Input.GetKey(KeyCode.RightArrow))
       {
           Debug.Log("Pressed right");
           _rigidBody.AddRelativeForce(Vector3.right * 5f);
       } 
    }
}
