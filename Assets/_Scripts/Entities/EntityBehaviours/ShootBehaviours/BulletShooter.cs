using UnityEngine;

public class BulletShooter : IShooter
{
    private Bullet _bulletPrefab;

    private float _shootForce;

    private Transform _muzzle;

    public BulletShooter(Bullet bulletPrefab, Transform muzzle, float shootForce)
    {
        _bulletPrefab = bulletPrefab;
        _muzzle = muzzle;
        _shootForce = shootForce;
    }

    public void Shoot()
    {
        Bullet bullet = Object.Instantiate(_bulletPrefab, _muzzle.position, _muzzle.rotation, null);
        bullet.Launch(_muzzle.forward * _shootForce);
    }
}
