using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CannonBullet : MonoBehaviour
{
    public float speed = 10;
    public float bulletDamage = 3;
    public Rigidbody2D rb;

    public void InitializeBullet(float speed = 0,float damage = 0)
    {
        if(speed != 0)
            this.speed = speed;

        if (damage != 0)
            this.bulletDamage = damage;
    }


    private void Reset()
    {
        var rb = GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            this.rb = rb;
            rb.gravityScale = 0;
        }
    }

}
