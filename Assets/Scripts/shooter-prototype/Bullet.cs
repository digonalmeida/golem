using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public void Initialize(ShotData shotData)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = shotData.Velocity;
    }
}
