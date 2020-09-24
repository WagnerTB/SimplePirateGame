using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chaser Enemy ", menuName = "Enemy/Chaser")]
public class EnemyChaser : AIBehaviour
{
    [Space]
    public float explosionDamage = 2;
    public float explosionForce = 10;
    public float explosionRange = 2;

    public GameObject explosionGO;

    public override void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            var bc = collider.gameObject.GetComponent<BasicController>();
            var rb = collider.gameObject.GetComponent<Rigidbody>();
            if(bc != null)
            {
                bc.Damage(explosionDamage);

                if (rb != null)
                {
                    var center = executedGo.gameObject.transform.position;
                    rb.AddExplosionForce(explosionForce, center, explosionRange);
                    Debug.Log("BOOM!");
                }

                Explode();
            }
        }
    }

    private void Explode()
    {
        var explosion = Instantiate(explosionGO);
        explosion.transform.position = executedGo.transform.position;
        explosion.transform.rotation = executedGo.transform.rotation;
        Destroy(explosion, .1f);

        Destroy(executedGo.gameObject);
    }
}
