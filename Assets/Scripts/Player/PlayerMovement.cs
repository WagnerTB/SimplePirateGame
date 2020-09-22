using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 30;
    public float maxSpeed = 30;
    public float rotateSpeed = 100;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        var Horizontal = Input.GetAxis("Horizontal");
        var Vertical = Input.GetAxis("Vertical");

        Move(Vertical);
        Rotate(Horizontal);
    }

    private void Move(float input)
    {
        var direction = transform.right * input * speed * Time.deltaTime;
        // rb.MovePosition(transform.position + direction);
        if(rb.velocity.magnitude < maxSpeed)
            rb.AddForce(direction); 
    }

    private void Rotate(float input)
    {
        Vector3 rotate = new Vector3(0, 0 , -input * rotateSpeed);

        Quaternion deltaRotation = Quaternion.Euler(rotate * Time.deltaTime);
        rb.MoveRotation(transform.rotation * deltaRotation);
    }




    private void Reset()
    {
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            this.rb = rb;
            rb.gravityScale = 0;
        }
    }
}
