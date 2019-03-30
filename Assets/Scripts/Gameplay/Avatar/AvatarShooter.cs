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

    public bool IsShooting { get; set; }

    public delegate void ShotDataDelegate(ShotData shotData);
    public event ShotDataDelegate OnShoot;

    private void Start()
    {
        StartCoroutine(ShootingCoroutine());
    }

    private void Shoot()
    {
        ShotData shotData = new ShotData(transform.forward * _bulletSpeed);

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
