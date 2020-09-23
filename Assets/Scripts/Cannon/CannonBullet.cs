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

    public GameObject explosionPrefab;

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

    public void OnTriggerEnter(Collider other)
    {
        var basicController = other.GetComponent<BasicController>();
        if (basicController != null)
        {
            basicController.Damage(bulletDamage);
        }

        var explosion = Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;
        explosion.transform.rotation = transform.rotation;
        Destroy(explosion, .1f);
        Destroy(this.gameObject);

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
