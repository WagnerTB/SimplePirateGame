using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform target;
    public AIBehaviour aIBehaviour;
    public AIMovement aIMovement;

    // Start is called before the first frame update
    void Start()
    {
        if(aIBehaviour != null)
        {
            aIBehaviour = Instantiate(aIBehaviour);

            aIBehaviour.executedGo = this;
            aIBehaviour.Start();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(aIBehaviour != null)
        {
            aIBehaviour.Update();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(aIBehaviour != null)
        {
            aIBehaviour.OnDrawGizmosSelected();
        }
    }

}
