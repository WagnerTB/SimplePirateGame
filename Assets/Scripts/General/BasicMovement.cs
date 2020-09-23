using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BasicMovement : MonoBehaviour
{
    public float speed = 30;
    public float maxSpeed = 30;
    public float rotateSpeed = 100;
    public Rigidbody rb;

    protected virtual void Move(float input)
    {
        var direction = transform.up* input * speed * Time.deltaTime;
        if (rb.velocity.magnitude < maxSpeed)
            rb.AddForce(direction);
    }

    protected virtual void Rotate(float input)
    {
        Vector3 rotate = new Vector3(0, 0, -input * rotateSpeed);
        Quaternion deltaRotation = Quaternion.Euler(rotate * Time.deltaTime);
        rb.MoveRotation(transform.rotation * deltaRotation);
    }

    public virtual void Stop()
    {
        rb.drag = 1;
        rb.angularDrag = 1;
    }

    private void Reset()
    {
        var rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            this.rb = rb;
            rb.useGravity = false;
        }
    }
}
