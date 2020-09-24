using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBehaviour : ScriptableObject
{
    [HideInInspector]
    public AIController executedGo;

    public float maxHp;

    public virtual void Start()
    {
        executedGo.target = GameManager.Instance.player.transform;

        executedGo.hp = maxHp;
        executedGo.maxHp = maxHp;
    }

    public virtual void Update() { }

    public virtual void OnCollisionEnter(Collision collision) { }

    public virtual void OnTriggerEnter(Collider collider) { }

    public virtual void OnDrawGizmosSelected() { }
}
