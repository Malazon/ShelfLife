using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedController : MonoBehaviour
{
    private Rigidbody playerRigidBody;

    [SerializeField] private float cooldown = 0.25f;
    [SerializeField] private float maxSpeed = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        this.playerRigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W))
        {
            input += Vector3.forward;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            input += Vector3.back;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            input += Vector3.left;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            input += Vector3.right;
        }

        var newSpeed = Mathf.Clamp(
            playerRigidBody.velocity.magnitude + maxSpeed / cooldown * Time.deltaTime, 
            0f,
            max: maxSpeed
            );

        playerRigidBody.velocity = playerRigidBody.rotation * Vector3.forward * newSpeed;
    }
}
