using UnityEngine;

public class PlayerSpeedController : MonoBehaviour
{
    [SerializeField] private float cooldown = 0.25f;
    [SerializeField] private float maxSpeed = 1f;



    // Update is called once per frame
    private void Update()
    {
        if (PlayerSingleton.Active == null) return;

        var player = PlayerSingleton.Active;

        if (CameraSingleton.Active == null) return;

        var camera = CameraSingleton.Active;

        var input = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) input += Vector3.forward;

        if (Input.GetKey(KeyCode.S)) input += Vector3.back;

        if (Input.GetKey(KeyCode.A)) input += Vector3.left;

        if (Input.GetKey(KeyCode.D)) input += Vector3.right;


        var newSpeed = Mathf.Clamp(
            player.RigidBody.velocity.magnitude + maxSpeed / cooldown * Time.deltaTime,
            0f,
            maxSpeed
        );

        var planeDirection = Vector3.ProjectOnPlane(camera.transform.rotation * Vector3.forward, Vector3.up);

        player.RigidBody.velocity = Quaternion.LookRotation(planeDirection) * Quaternion.Euler(0, camera.transform.eulerAngles.y, 0) * input * newSpeed;

        #region Animation Signals
        player.PlayerAnimationCodeHook.SetForward(Vector3.Dot(input, player.transform.forward));
        player.PlayerAnimationCodeHook.SetStrafe(Vector3.Dot(input, player.transform.right));

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