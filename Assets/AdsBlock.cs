using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;

public class AdsBlock : MonoBehaviour
{
    private float _life = 1;

    private void Start()
    {
        UpdateColor();
    }
    
    private void TakeHit()
    {
        _life-=0.25f;
        if (_life <= 0)
        {
            Destroy(gameObject);
            return;
        }
        
        UpdateColor();
    }

    private void UpdateColor()
    {
        GetComponent<Renderer>().material.color = new Color(1, _life, _life);
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        var bullet = other.GetComponent<Bullet>();
        if (bullet == null)
        {
            return;
        }

        TakeHit();
        Destroy(other.gameObject);
    }
}
