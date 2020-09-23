using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] BasicController _basicController;

    [SerializeField] private int _statesCount;
    [SerializeField] private Animator animator;


    [SerializeField] private SpriteRenderer ShipSpriteRenderer;
    [SerializeField] private SpriteRenderer SailSpriteRenderer;

    private float percentagePerState = 0;

    private void OnEnable()
    {
        if (_basicController != null)
        {
            _basicController.onDamage += UpdateShip;
            _basicController.onHeal += UpdateShip;

            percentagePerState = 1f / _statesCount;
        }
    }

    private void OnDestroy()
    {
        if (_basicController != null)
        {
            _basicController.onDamage -= UpdateShip;
            _basicController.onHeal -= UpdateShip;
        }
    }


    private void UpdateShip(float amount)
    {
        var currentIntegrityPercentage = (_basicController.hp / _basicController.maxHp);
        animator.SetFloat("Integrity", currentIntegrityPercentage);
    }
}
