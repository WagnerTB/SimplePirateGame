using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BasicController
{
    public Cannon frontalCannon;
    public Cannon[] lateralCannons;

    // Update is called once per frame
    void Update()
    {
        CheckShoot();
    }

    public void CheckShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FrontalShoot();
        }

        if (Input.GetMouseButtonDown(1))
        {
            LateralShoot();
        }
    }

    public void FrontalShoot()
    {
        if(frontalCannon != null)
        {
            frontalCannon.ShootCannon();
        }
    }

    public void LateralShoot()
    {
        foreach (var cannon in lateralCannons)
        {
            if(cannon != null)
            {
                cannon.ShootCannon();
            }
        }
    }
}
