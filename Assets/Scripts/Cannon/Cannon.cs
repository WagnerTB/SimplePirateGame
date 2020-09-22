using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [Header("Cannon")]
    public float fireRate = 1;
    [SerializeField] private bool canShoot;
    private float _elapsedTime = 0;

    [Header("Cannon Bullet")]
    public float bulletSpeed = 10;
    public float bulletDamage = 10;
    public Transform bulletPivot;
    public CannonBullet cannonBullet;

    private void FixedUpdate()
    {
        FireCooldown();
    }

    private void FireCooldown()
    {
        if (canShoot) return;

        if(_elapsedTime < fireRate)
        {
            _elapsedTime += Time.deltaTime;
        }
        else if(_elapsedTime >= fireRate && !canShoot)
        {
            _elapsedTime = 0;
            canShoot = true;
        }
    }

    public bool ShootCannon()
    {
        if(canShoot)
        {
            Debug.Log("Shoot!");
            canShoot = false;
            var bullet = Instantiate(cannonBullet,bulletPivot.position,bulletPivot.rotation);
            bullet.transform.localScale = bulletPivot.lossyScale;
            bullet.InitializeBullet(bulletSpeed, bulletDamage);
            return true;
        }
        else
        {
            return false;
        }
    }

  

}
