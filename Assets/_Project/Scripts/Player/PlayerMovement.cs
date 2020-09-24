using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : BasicMovement
{
    [Space]
    [Header("Movement Limit Config")]
    public Bounds playerMovementLimits;

    [Header("References")]
    public PlayerController playerController;

    private void FixedUpdate()
    {
        RestrictMovement();

        if (playerController.isActive)
        {
            var Horizontal = Input.GetAxis("Horizontal");
            var Vertical = Input.GetAxis("Vertical");

            Move(Vertical);
            Rotate(Horizontal);
        }
    }

    private void RestrictMovement()
    {
        var clampedPosition = new Vector3(Mathf.Clamp(transform.position.x, playerMovementLimits.center.x - playerMovementLimits.extents.x, playerMovementLimits.center.x + playerMovementLimits.extents.x),
                                          transform.position.y,
                                          Mathf.Clamp(transform.position.z, playerMovementLimits.center.z - playerMovementLimits.extents.z, playerMovementLimits.center.z + playerMovementLimits.extents.z));
        transform.position = clampedPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Vector3.zero, playerMovementLimits.extents * 2);
    }
}
