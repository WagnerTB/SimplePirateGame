using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CannonBullet : MonoBehaviour
{
    public float speed = 10;
    public float bulletDamage = 3;
    public float timeToDestroy = 3;
    public Rigidbody rb;

    private void Start()
    {
        Destroy(this.gameObject, timeToDestroy);
    }

    public void InitializeBullet(float speed = 0,float damage = 0)
    {
        if(speed != 0)
            this.speed = speed;

        if (damage != 0)
            this.bulletDamage = damage;


        var direction = transform.up * speed;
        rb.AddForce(direction);
    }


    private void Reset()
    {
        var rb = GetComponent<Rigidbody>();
        if(rb != null)
        {
            this.rb = rb;
            rb.useGravity = false;
        }
    }

}
