using Cinemachine.Utility;
using UnityEngine;

public class PlayerSpeedController : MonoBehaviour
{
    [SerializeField] private float cooldown = 0.25f;
    [SerializeField] private float maxSpeed = 1f;



    // Update is called once per frame
    private void Update()
    {
        // Return if the game is paused.
        if (PauseMenuSingleton.Paused) return;

        // Return to wait for the singleton to update.
        // Or if the player has died.
        if (PlayerSingleton.Combatant == null || PlayerSingleton.Combatant.HasDied) return;

        // Return if the Camera isn't active.
        if (CameraSingleton.Active == null) return;

        // Define known variables.
        var playerRigidBody = PlayerSingleton.RigidBody;
        var camera = CameraSingleton.Active.Camera;
        var input = Vector3.zero;

        // Grab the raw input.
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");
        
        // Calculate the target speed
        var targetSpeed = Mathf.Max(input.x, input.z) * maxSpeed;
        
        // Grab the current speed.
        var currentSpeed = PlayerSingleton.RigidBody.velocity.magnitude;

        // Calculate the new player speed.
        var newSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, maxSpeed / cooldown);

        // Calculate the translation to forward.
        var newDirection = camera.transform.TransformDirection(input).normalized;
        
        // Calculate the new velocity
        var newVelocity = newDirection * newSpeed;
        
        // Check on the falling.
        if (!Physics.Raycast(transform.position + Vector3.up, Vector3.down, 3))
        {
            newVelocity.y = -5;
        }
        
        // Apply the new velocity.
        playerRigidBody.velocity = newVelocity;

        #region Animation Signals
        PlayerSingleton.PlayerAnimationCodeHook.SetForward(Vector3.Dot(playerRigidBody.velocity, playerRigidBody.transform.forward));
        PlayerSingleton.PlayerAnimationCodeHook.SetStrafe(Vector3.Dot(playerRigidBody.velocity, playerRigidBody.transform.right));

        if (newVelocity.AlmostZero())
        {
            PlayerSingleton.PlayerAnimationCodeHook.SetMoving(true);
        }
        else
        {
            PlayerSingleton.PlayerAnimationCodeHook.SetMoving(false);
        }
        #endregion
    }
}