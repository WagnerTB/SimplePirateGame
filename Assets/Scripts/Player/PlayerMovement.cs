using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : BasicMovement
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        var Horizontal = Input.GetAxis("Horizontal");
        var Vertical = Input.GetAxis("Vertical");

        Move(Vertical);
        Rotate(Horizontal);
    }

   
}
