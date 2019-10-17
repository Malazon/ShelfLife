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

        if (playerCombatant.HasDied)
        {
            playerRigidBody.angularVelocity = Vector3.zero;
            return;
        };

        // Clear the angular velocity
        playerRigidBody.angularVelocity = Vector3.zero;
        
        // Ensure the player is upright.
        var currentDirection = playerRigidBody.transform.forward.ProjectOntoPlane(Vector3.up);
        playerRigidBody.rotation = Quaternion.LookRotation(currentDirection);
        
        if (CameraSingleton.MouseWorldPosition != null)
        {
            // If the mouse position exists, rotate towards it.
            var safeMousePosition = (Vector3)CameraSingleton.MouseWorldPosition;

            // Find the desired target direction
            var targetDirection = (safeMousePosition - playerRigidBody.position).ProjectOntoPlane(Vector3.up);

            var angle = Vector3.SignedAngle(currentDirection, targetDirection, Vector3.up);
            var step = rotationRate * Time.deltaTime;
            
            playerRigidBody.angularVelocity = Vector3.up * (angle / step);
        }
    }
}