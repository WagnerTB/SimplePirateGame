using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : BasicController
{
    public Transform target;
    public AIBehaviour aIBehaviour;
    public AIMovement aIMovement;

    // Start is called before the first frame update
    void Start()
    {
        if(isActive && aIBehaviour != null)
        {
            aIBehaviour = Instantiate(aIBehaviour);

            aIBehaviour.executedGo = this;
            aIBehaviour.Start();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive && aIBehaviour != null)
        {
            aIBehaviour.Update();
        }
    }

    public override void Die()
    {
        base.Die();
        Destroy(this.gameObject, 3);
    }

    private void OnDrawGizmosSelected()
    {
        if(aIBehaviour != null)
        {
            aIBehaviour.OnDrawGizmosSelected();
        }
    }

}
