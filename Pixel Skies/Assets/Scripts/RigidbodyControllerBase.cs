using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class RigidbodyControllerBase : MonoBehaviour
{
    protected Rigidbody _rigidbody;
    protected AudioSource _audioSource;
    // Start is called before the first frame update
    public virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource != null)
            _audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_rigidbody != null)
        {
            HandlePhysics();
        }
    }

    protected virtual void HandlePhysics()
    {
        throw new System.NotImplementedException();
    }
}
