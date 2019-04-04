using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarShooter : MonoBehaviour
{
    [SerializeField]
    private Bullet _bulletPrefab = null;

    [SerializeField]
    private float _bulletSpeed = 10.0f;

    [SerializeField]
    private float _shootingInterval = 0.5f;

    [SerializeField]
    private float _spread = 0.0f;

    public bool IsShooting { get; set; }

    public delegate void ShotDataDelegate(ShotData shotData);
    public event ShotDataDelegate OnShoot;

    private void Start()
    {
        StartCoroutine(ShootingCoroutine());
    }

    private void Shoot()
    {

        var direction = Quaternion.EulerRotation(0, UnityEngine.Random.Range(-_spread, _spread), 0) * transform.forward; 
        ShotData shotData = new ShotData(direction * _bulletSpeed * (AvatarController.Inverted ? -1 : 1));

        var bullet = Instantiate(_bulletPrefab).GetComponent<Bullet>();
        bullet.transform.position = transform.position;
        bullet.Initialize(shotData);
        if(OnShoot != null)
        {
            OnShoot.Invoke(shotData);
        }
    }

    private IEnumerator ShootingCoroutine()
    {
        for(;;)
        {
            while(!IsShooting)
            {
                yield return null;
            }
            Shoot();
            yield return new WaitForSeconds(_shootingInterval);
        }
    }
}
