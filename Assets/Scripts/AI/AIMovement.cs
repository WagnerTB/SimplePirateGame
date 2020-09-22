using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : BasicMovement
{
    public AIController aIController;
    public NavMeshAgent agent;
    public Transform faceTarget;

    public bool isStopped = false;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        //Se eu deixar dentro do objeto ele "anda" 2 vezes, pq ele esta como filho nosso 
        // dai ele fica muito mais rapido que realmente é
        agent.transform.SetParent(null);
        faceTarget = agent.transform;
        if(aIController.target != null)
        {
            agent.SetDestination(aIController.target.position);
        }
    }

    private void FixedUpdate()
    {
        ShipFollowAgent();

        if(aIController.target.position != agent.destination)
        {
            agent.SetDestination(aIController.target.position);
            agent.isStopped = false;

        }
        if (agent.destination == transform.position) return;
        if (agent.path.status == NavMeshPathStatus.PathComplete || agent.path.status == NavMeshPathStatus.PathPartial)
        {
            if(agent.remainingDistance < range)
            {
                agent.velocity = Vector3.zero;
                agent.isStopped = true;
                Debug.Log("Cheguei na distancia minima!");
            }
        }
    }

    public void Stop()
    {
        rb.drag = 1;
        rb.angularDrag = 1;
        isStopped = true;
    }

    public void Release()
    {
        rb.drag = .1f;
        rb.angularDrag = .05f;
        isStopped = false;
    }

    private void ShipFollowAgent()
    {
        var rbPos = new Vector2(transform.position.x, transform.position.z);
        var agentPos = new Vector2(agent.transform.position.x,agent.transform.position.z);
        var agentDistance = Vector2.Distance(rbPos, agentPos);

        if (agentDistance < 2)
        {
            agent.isStopped = false;
        }
        else if (agentDistance >= 3) 
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }

        if (agentDistance > .2f)
        {
            Rotate(0);
            Move(1 * ((isStopped)? 0 :  1));
        }
    }

    private void OnDrawGizmos()
    {
        if(aIController != null && aIController.target != null)
        {
            Gizmos.DrawWireSphere(agent.transform.position, range);
        }
    }

    protected override void Rotate(float input)
    {
        var direction = (faceTarget.transform.position - transform.position).normalized;
        var rotatePosition = Quaternion.LookRotation(Vector3.up, direction);
        transform.rotation = rotatePosition;
    }


}
