using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationController : MonoBehaviour
{

    [SerializeField] private float rotationRate = 720f;


    // Update is called once per frame
    void Update()
    {
        if (PlayerSingleton.Active is null)
        {
            return;
        }

        var player = PlayerSingleton.Active;
        
        // Clear the angular velocity
        player.RigidBody.angularVelocity = Vector3.zero;

        if (CameraSingleton.Active is null)
        {
            return;
        }

        var camera = CameraSingleton.Active;
        
        var mouseRay = camera.Camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit raycastHit;

        if (Physics.Raycast(mouseRay, out raycastHit, 100, LayerMask.GetMask("Floor")))
        {
            // If the ground is hit, rotate towards the screen
            Debug.DrawRay(raycastHit.point, Vector3.up, Color.red);
            
            var planeTargetDirection = Vector3.ProjectOnPlane(raycastHit.point - player.RigidBody.position, Vector3.up);
            var planeCurrentDirection = Vector3.ProjectOnPlane(player.RigidBody.rotation * Vector3.forward, Vector3.up);

            var angleToRotate = Mathf.Clamp(
                Vector3.SignedAngle(planeCurrentDirection, planeTargetDirection, Vector3.up),
                -rotationRate*Time.deltaTime,
                rotationRate*Time.deltaTime
                );

            player.RigidBody.rotation = Quaternion.LookRotation(planeCurrentDirection, Vector3.up) * Quaternion.AngleAxis(angleToRotate, Vector3.up);
        }
        else
        {
            // If the ground is not hit, ensure the pawn is upright.
            var planeTargetDirection = Vector3.ProjectOnPlane(raycastHit.point - player.RigidBody.position, Vector3.up);
            player.RigidBody.rotation = Quaternion.LookRotation(planeTargetDirection, Vector3.up);
        }
        
        Debug.DrawRay(mouseRay.origin, mouseRay.direction*100f);
    }
}
