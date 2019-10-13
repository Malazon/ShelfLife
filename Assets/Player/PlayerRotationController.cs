using UnityEngine;

public class PlayerRotationController : MonoBehaviour
{
    [SerializeField] private float rotationRate = 720f;


    // Update is called once per frame
    private void Update()
    {
        if (PlayerSingleton.Active == null) return;

        var player = PlayerSingleton.Active;

        // Clear the angular velocity
        player.RigidBody.angularVelocity = Vector3.zero;
        
        if (player.MouseWorldPosition.Position == null)
        {
            // If the ground is not hit, ensure the pawn is upright.
            var planeTargetDirection = Vector3.ProjectOnPlane(player.RigidBody.rotation * Vector3.forward, Vector3.up);
            player.RigidBody.rotation = Quaternion.LookRotation(planeTargetDirection, Vector3.up);
        }
        else
        {
            Vector3 mousePosition = player.MouseWorldPosition.Position ?? player.RigidBody.position;
            // If the ground is hit, rotate towards the screen
            var planeTargetDirection = Vector3.ProjectOnPlane(mousePosition - player.RigidBody.position, Vector3.up);
            var planeCurrentDirection = Vector3.ProjectOnPlane(player.RigidBody.rotation * Vector3.forward, Vector3.up);

            var angleToRotate = Mathf.Clamp(
                Vector3.SignedAngle(planeCurrentDirection, planeTargetDirection, Vector3.up),
                -rotationRate * Time.deltaTime,
                rotationRate * Time.deltaTime
            );

            player.RigidBody.rotation = Quaternion.LookRotation(planeCurrentDirection, Vector3.up) *
                                        Quaternion.AngleAxis(angleToRotate, Vector3.up);
        }

    }
}