using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public float size;

    public Vector3 RequestPosition()
    {
        var point = UnityEngine.Random.insideUnitSphere * size;
        var pos = transform.position + point;
        pos.y = 0;
        return pos;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, size);
    }
}
