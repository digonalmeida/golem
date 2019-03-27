using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;

public class AdsBlock : MonoBehaviour
{
    [SerializeField] 
    private Renderer _renderer;

    private Collider _collider;
    private float _life = 1;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        UpdateColor();
    }
    
    public void TakeHit()
    {
        if(_life <= 0)
        {
            return;
        }

        _life-=0.25f;
        if (_life <= 0)
        {
            StartCoroutine(RespawnCoroutine());
            return;
        }
        
        UpdateColor();
    }

    private void UpdateColor()
    {
        float gray = 0.5f - ((1-_life) * 0.5f);
        float red = 0.5f + ((1-_life) * 0.5f);
        _renderer.material.color = new Color(red, gray, gray);
    }
    
    private IEnumerator RespawnCoroutine()
    {
        _collider.enabled = false;
        _renderer.enabled = false;
        yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        _collider.enabled = true;
        _renderer.enabled = true;
        _life = 1;
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
