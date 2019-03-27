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
        transform.forward = shotData.Velocity.normalized;
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger " + other.name);
    }

    private void OnCollisionEnter(Collision other)
    {
        var adsBlock = other.collider.GetComponent<AdsBlock>();
        if (adsBlock != null)
        {
            adsBlock.TakeHit();
        }
        
        Destroy(gameObject);
    }
}
