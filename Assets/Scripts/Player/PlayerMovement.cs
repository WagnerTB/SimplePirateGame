using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : BasicMovement
{
    public PlayerController playerController;
    public Bounds bounds;



    private void FixedUpdate()
    {

        RestrictMovement();
        if (playerController.isAlive)
        {
            var Horizontal = Input.GetAxis("Horizontal");
            var Vertical = Input.GetAxis("Vertical");

            Move(Vertical);
            Rotate(Horizontal);
        }
    }

    private void RestrictMovement()
    {
        var clampedPosition = new Vector3(Mathf.Clamp(transform.position.x, bounds.center.x - bounds.extents.x, bounds.center.x + bounds.extents.x),
                                          transform.position.y,
                                          Mathf.Clamp(transform.position.z, bounds.center.z - bounds.extents.z, bounds.center.z + bounds.extents.z));
        transform.position = clampedPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Vector3.zero, bounds.extents * 2);
    }
}
