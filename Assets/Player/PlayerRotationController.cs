using Cinemachine.Utility;
using UnityEngine;

public class PlayerRotationController : MonoBehaviour
{
    [SerializeField] private float rotationRate = 720f;


    // Update is called once per frame
    private void Update()
    {
        if (PauseMenuSingleton.Paused) return;
        if (PlayerSingleton.Active == null) return;

        var playerRigidBody = PlayerSingleton.RigidBody;
        var playerCombatant = PlayerSingleton.Combatant;

        if (playerCombatant.HasDied) return;

        // Clear the angular velocity
        playerRigidBody.angularVelocity = Vector3.zero;
        
        if (CameraSingleton.MouseWorldPosition == null)
        {
            // If the mouse position does not exist, ensure the player is upright.
            var currentDirection = playerRigidBody.transform.forward.ProjectOntoPlane(Vector3.up);
            playerRigidBody.rotation = Quaternion.LookRotation(currentDirection);
        }
        else
        {
            // If the mouse position exists, rotate towards it.
            var safeMousePosition = (Vector3)CameraSingleton.MouseWorldPosition;

            var currentDirection = playerRigidBody.transform.forward.ProjectOntoPlane(Vector3.up);
            var targetDirection = (safeMousePosition - playerRigidBody.position).ProjectOntoPlane(Vector3.up);

            var angleToRotate = Mathf.Clamp(
                Vector3.SignedAngle(targetDirection, currentDirection, Vector3.up),
                -rotationRate * Time.deltaTime,
                rotationRate * Time.deltaTime
            );

            playerRigidBody.rotation = Quaternion.LookRotation(currentDirection) *
                                       Quaternion.Euler(0, angleToRotate, 0);
        }

    }
}