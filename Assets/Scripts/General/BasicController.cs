using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicController : MonoBehaviour
{
    public float hp = 10;
    public float maxHp = 10;
    public bool isActive = true;

    public delegate void StatusChange(float amountChange);
    public StatusChange onHeal;
    public StatusChange onDamage;
    public StatusChange onDie;

    public void Awake()
    {
        RegisterEvent();
    }

    protected virtual void RegisterEvent()
    {
        GameManager.onEnd += EndGame;
    }

    protected virtual void UnRegisterEvent()
    {
        GameManager.onEnd -= EndGame;
    }

    private void OnDestroy()
    {
        UnRegisterEvent();
    }

public virtual void EndGame()
    {
        isActive = false;
    }


    public void Heal(float amount)
    {
        if(hp + amount > maxHp)
        {
            hp = maxHp;
        }
        else
        {
            hp += amount;
        }
       
        onHeal?.Invoke(amount);
    }

    public void Damage(float amount)
    {
        hp -= amount;
        if(hp <= 0)
        {
            Die();
        }
        onDamage?.Invoke(amount);
    }

    public virtual void Die()
    {
        isActive = false;
    }


}
