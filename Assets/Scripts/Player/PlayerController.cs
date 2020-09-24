using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BasicController
{
    public int playerScore { get; private set; }

    [Header("Info")]
    [SerializeField] private int _playerScore = 0;

    [Space]
    [Header("Player Config")]
    [SerializeField] private float _frontalCannonDamage;
    [SerializeField] private float _lateralCannonsDamage;

    [Header("References")]
    [SerializeField] private Cannon[] _lateralCannons;
    [SerializeField] private Cannon _frontalCannon;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerUI _playerUI;

    protected override void RegisterEvent()
    {
        base.RegisterEvent();

        GameManager.onEndGame += _playerMovement.Stop;
        GameManager.onEnemyDie += Score;
    }

    protected override void UnRegisterEvent()
    {
        base.UnRegisterEvent();

        GameManager.onEndGame -= _playerMovement.Stop;
        GameManager.onEnemyDie -= Score;
    }

    private void Start()
    {
        UpdateCannonsDamage();
    }
        
    private void UpdateCannonsDamage()
    {
        _frontalCannon.bulletDamage = _frontalCannonDamage;
        foreach (var cannon in _lateralCannons)
        {
            cannon.bulletDamage = _lateralCannonsDamage;
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
        if (_frontalCannon != null)
        {
            _frontalCannon.ShootCannon();
        }
    }

    public void LateralShoot()
    {
        foreach (var cannon in _lateralCannons)
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
        _playerUI.UpdateScore(playerScore);
    }
}
