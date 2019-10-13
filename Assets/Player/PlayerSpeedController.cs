using UnityEngine;

public class PlayerSpeedController : MonoBehaviour
{
    [SerializeField] private float cooldown = 0.25f;
    [SerializeField] private float maxSpeed = 1f;



    // Update is called once per frame
    private void Update()
    {
        if (PauseMenuSingleton.Paused) return;

        if (PlayerSingleton.Active == null) return;

        var player = PlayerSingleton.Active;

        if (player.Combatant.HasDied) return;

        if (CameraSingleton.Active == null) return;

        var camera = CameraSingleton.Active;

        var input = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) input += Vector3.back;

        if (Input.GetKey(KeyCode.S)) input += Vector3.forward;

        if (Input.GetKey(KeyCode.A)) input += Vector3.right;

        if (Input.GetKey(KeyCode.D)) input += Vector3.left;

        // This won't make sense with analog input
        if (input.x != 0 && input.z != 0)
        {
            input.x = Mathf.Clamp(input.x, -0.707f, 0.707f);
            input.z = Mathf.Clamp(input.z, -0.707f, 0.707f);
        }


        var newSpeed = Mathf.Clamp(
            player.RigidBody.velocity.magnitude + maxSpeed / cooldown * Time.deltaTime,
            0f,
            maxSpeed
        );

        var cameraRotation = camera.Camera.worldToCameraMatrix.MultiplyVector(Vector3.forward);

        cameraRotation = Vector3.ProjectOnPlane(cameraRotation, Vector3.up).normalized;
        
        Debug.DrawRay(player.transform.position, cameraRotation, Color.red, 0);

        player.RigidBody.velocity = Quaternion.LookRotation(cameraRotation, Vector3.up) * input * newSpeed;
        
        // Check on the falling.
        if (!Physics.Raycast(transform.position + Vector3.up, Vector3.down, 3))
        {
            player.RigidBody.velocity += Vector3.down * 5f;
        }

        #region Animation Signals
        player.PlayerAnimationCodeHook.SetForward(Vector3.Dot(player.RigidBody.velocity, player.transform.forward));
        player.PlayerAnimationCodeHook.SetStrafe(Vector3.Dot(player.RigidBody.velocity, player.transform.right));

        if (input != Vector3.zero)
        {
            player.PlayerAnimationCodeHook.SetMoving(true);
        }
        else
        {
            player.PlayerAnimationCodeHook.SetMoving(false);
        }
        #endregion
    }
}