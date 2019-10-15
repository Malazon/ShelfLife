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
        var targetSpeed = Mathf.Abs((input.x * input.x > input.z * input.z ? input.x : input.z) * maxSpeed);

        // Grab the current speed.
        var currentSpeed = PlayerSingleton.RigidBody.velocity.magnitude;

        // Calculate the new player speed.
        var maxDelta = (maxSpeed / cooldown) * Time.deltaTime;
        var delta = Mathf.Clamp(targetSpeed - currentSpeed, -maxDelta, maxDelta);
        var newSpeed = Mathf.Clamp(currentSpeed + delta, 0, maxSpeed);

        // Calculate the translation to forward.
        var cameraTransform = camera.cameraToWorldMatrix;
        var axisX = cameraTransform.MultiplyVector(Vector3.right);
        var axisY = cameraTransform.MultiplyVector(Vector3.up);
        var axisZ = cameraTransform.MultiplyVector(Vector3.back);
        
        // Useful for visualizing
        // Debug.DrawRay(playerRigidBody.position, axisX*5, Color.red);
        // Debug.DrawRay(playerRigidBody.position, axisY*5, Color.blue);
        // Debug.DrawRay(playerRigidBody.position, axisZ*5, Color.green);

        Vector3 transformedInput;
        if (Vector3.Dot(axisZ, Vector3.up) < 0.1f)
        {
            // Only use the Y axis if we are almost straight down.
            transformedInput = Quaternion.LookRotation(axisY.ProjectOntoPlane(Vector3.up))*input;
        }
        else
        {
            transformedInput = Quaternion.LookRotation(axisZ.ProjectOntoPlane(Vector3.up))*input;
        }
        Debug.DrawRay(playerRigidBody.position, transformedInput*5, Color.white);
        
        // Calculate the new velocity
        var newVelocity = transformedInput * newSpeed;
        
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