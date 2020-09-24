using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Ranged", menuName = "Enemy/Ranged")]
public class RangedEnemy : AIBehaviour
{
    [Space]
    public float minDistance = 2;
    public float cannonFireRate = 1;
    public float bulletDamage = 1;
    private Cannon[] cannons;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        cannons = executedGo.GetComponentsInChildren<Cannon>();
        foreach (var cannon in cannons)
        {
            cannon.fireRate = cannonFireRate;
            cannon.bulletDamage = bulletDamage;
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        CloseEnougth();
    }

    public void CloseEnougth()
    {
        var rbPos = new Vector2(executedGo.transform.position.x, executedGo.transform.position.z);
        var targetPos = new Vector2(executedGo.target.transform.position.x, executedGo.target.transform.position.z);
        if (Vector2.Distance(rbPos, targetPos) < minDistance)
        {
            executedGo.aIMovement.faceTarget = executedGo.target.transform;
            executedGo.aIMovement.Stop();
            ShootCannons();
        }
        else
        {
            if(executedGo.aIMovement.faceTarget != executedGo.aIMovement.agent)
            {
                executedGo.aIMovement.faceTarget = executedGo.aIMovement.agent.transform;
                executedGo.aIMovement.Release();

            }
        }
    }

    public bool ShootCannons()
    {
        foreach (var cannon in cannons)
        {
            if(cannon != null)
            {
              cannon.ShootCannon();
            }
        }
        return true;
    }

    public override void OnDrawGizmosSelected()
    {
        if(executedGo != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(executedGo.transform.position, minDistance);
        }
    }

   
}
