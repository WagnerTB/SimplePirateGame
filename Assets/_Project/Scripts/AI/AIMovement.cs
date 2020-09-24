using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : BasicMovement
{
    [Header("AI Config")]
    [SerializeField] private bool _isStopped = false;
    [SerializeField] private float _stopDistance;
    [SerializeField] private float _agentMaxDistance = 3;
    [SerializeField] private float _deathZone = .2f;
    [SerializeField] private float _faceRotateSpeed = 3;

    [Header("References")]
    [SerializeField] private AIController _aiController;
    public NavMeshAgent agent;
    public Transform faceTarget;

    void Start()
    {
        agent.transform.SetParent(null);
        faceTarget = agent.transform;
        agent.stoppingDistance = _stopDistance;
        if(_aiController.target != null)
        {
            agent.SetDestination(_aiController.target.position);
        }
    }

    private void FixedUpdate()
    {
        if (!_aiController.isActive)
        {
            Stop();
            return;
        }

        ShipFollowAgent();
        agent.SetDestination(_aiController.target.position);

        if(NavMesh.Raycast(agent.transform.position,transform.position,out NavMeshHit hit,NavMesh.AllAreas))
        {
            agent.Warp(transform.position);
        }
    }

    public override void Stop()
    {
        base.Stop();
        _isStopped = true;
    }

    public void Release()
    {
        rb.drag = .1f;
        rb.angularDrag = .05f;
        _isStopped = false;
    }

    private void ShipFollowAgent()
    {
        var rbPos = new Vector2(transform.position.x, transform.position.z);
        var agentPos = new Vector2(agent.transform.position.x,agent.transform.position.z);
        var agentDistance = Vector2.Distance(rbPos, agentPos);

        if (agentDistance < _agentMaxDistance)
        {
            agent.isStopped = false;
        }
        else if (agentDistance >= _agentMaxDistance) 
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }

        if (agentDistance > _deathZone)
        {
            Rotate(0);
            Move(1 * ((_isStopped)? 0 :  1));
        }
    }

    private void OnDrawGizmos()
    {
        if(_aiController != null && _aiController.target != null)
        {
            Gizmos.DrawWireSphere(agent.transform.position, _stopDistance);
        }
    }

    protected override void Rotate(float input)
    {
        var direction = (faceTarget.transform.position - transform.position).normalized;
        var rotatePosition = Quaternion.LookRotation(-Vector3.up, direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotatePosition, Time.deltaTime * _faceRotateSpeed);

    }


}
