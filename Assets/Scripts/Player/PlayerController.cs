using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BasicController
{
    public PlayerMovement playerMovement;

    public Cannon frontalCannon;
    public Cannon[] lateralCannons;

    protected override void RegisterEvent()
    {
        base.RegisterEvent();

        GameManager.onEnd += playerMovement.Stop;
    }

    protected override void UnRegisterEvent()
    {
        base.UnRegisterEvent();

        GameManager.onEnd -= playerMovement.Stop;
    }


    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            CheckShoot();
        }
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
