using System.Collections;
using UnityEngine;

public class AdsBlock : MonoBehaviour
{
    [SerializeField] 
    private Renderer _renderer;

    [SerializeField] 
    private Vector3 _direction = Vector3.zero;

    [SerializeField] 
    private float _speed = 1.0f;

    private Collider _collider;
    private float _life = 1;
    private Vector3 _startPosition;


    private void Start()
    {
        _collider = GetComponent<Collider>();
        UpdateColor();
        _startPosition = transform.position;
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
        Hide();
        yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        Show();
    }

    private void Hide()
    {
        _collider.enabled = false;
        _renderer.enabled = false;
    }

    private void Show()
    {
        transform.position = _startPosition;
        _collider.enabled = true;
        _renderer.enabled = true;
        _life = 1;
        UpdateColor();
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

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.position += _direction.normalized * _speed * Time.deltaTime;
    }
}
