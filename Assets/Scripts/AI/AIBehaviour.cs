using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBehaviour : ScriptableObject
{
    [HideInInspector]
    public AIController executedGo;

    public abstract void Start();
    public abstract void Update();

    public abstract void OnDrawGizmosSelected();
}
