using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public BasicController ownerController;
    public float fireRate = 1;
    public float bulletSpeed = 10;
    public Transform bulletPivot;
    public CannonBullet cannonBullet;

    [SerializeField]
    private bool canShoot;
    private float elapsedTime = 0;

    private void Update()
    {
        FireCooldown();
    }

    private void FireCooldown()
    {
        if(elapsedTime < fireRate)
        {
            elapsedTime += Time.deltaTime;
        }
        else if(elapsedTime >= fireRate && !canShoot)
        {
            elapsedTime = 0;
            canShoot = true;
        }
    }

    public bool ShootCannon()
    {
        if(canShoot)
        {
            var bullet = Instantiate(cannonBullet,bulletPivot.position,bulletPivot.rotation);
            bullet.InitializeBullet(bulletSpeed, ownerController.damage);
            return true;
        }
        else
        {
            return false;
        }
    }

  

}
