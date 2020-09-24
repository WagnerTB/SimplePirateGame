﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BasicController
{
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private float frontalCannonDamage;
    [SerializeField] private float lateralCannonsDamage;

    [SerializeField] private Cannon frontalCannon;
    [SerializeField] private Cannon[] lateralCannons;

    public int playerScore { get; private set; }
    [SerializeField] private int _playerScore = 0;
    [SerializeField] private PlayerUI playerUI;

    protected override void RegisterEvent()
    {
        base.RegisterEvent();

        GameManager.onEndGame += playerMovement.Stop;
        GameManager.onEnemyDie += Score;
    }

    protected override void UnRegisterEvent()
    {
        base.UnRegisterEvent();

        GameManager.onEndGame -= playerMovement.Stop;
        GameManager.onEnemyDie -= Score;
    }

    private void Start()
    {
        UpdateCannonsDamage();
    }
        
    private void UpdateCannonsDamage()
    {
        frontalCannon.bulletDamage = frontalCannonDamage;
        foreach (var cannon in lateralCannons)
        {
            cannon.bulletDamage = lateralCannonsDamage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
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
        if (frontalCannon != null)
        {
            frontalCannon.ShootCannon();
        }
    }

    public void LateralShoot()
    {
        foreach (var cannon in lateralCannons)
        {
            if (cannon != null)
            {
                cannon.ShootCannon();
            }
        }
    }

    public override void Die()
    {
        base.Die();
        GameManager.EndGame();
    }

    public void Score()
    {
        playerScore++;
        _playerScore = playerScore;
        playerUI.UpdateScore(playerScore);
    }
}
