using UnityEngine;

public class GravityGun : MonoBehaviour
{
    [SerializeField] private GameObject VisibleSphere;
    [SerializeField] private float MoveSpeed = 25;
    [SerializeField] private float ThrowSpeed = 25;
    private Rigidbody target;

    private void Update()
    {
        if (PlayerSingleton.Active is null) return;
        var player = PlayerSingleton.Active;

        if (Input.GetMouseButtonDown(1)) // Pick up object
        {
            Debug.Log("Pick Up");
            if (target != null)
            {
                target.isKinematic = false;
                target = null;
            }

            VisibleSphere.SetActive(true);

            var colliders =
                Physics.OverlapSphere(VisibleSphere.transform.position, VisibleSphere.transform.localScale.x);

            float targetDistance = 10000f;

            foreach (var collider in colliders)
            {
                if (!collider.TryGetComponent(out Rigidbody colliderRigidBody)) continue;
                
                var colliderDistance = Vector3.Distance(colliderRigidBody.position, VisibleSphere.transform.position);

                if (target is null || colliderDistance < targetDistance)
                {
                    target = colliderRigidBody;
                    targetDistance = colliderDistance;
                }
            }

            if (target is null)
            {
                VisibleSphere.SetActive(true);
            }
            else
            {
                target.isKinematic = true;
                VisibleSphere.SetActive(false);
            }
        }
        else if (Input.GetMouseButtonDown(0)) // Throw Object
        {
            Debug.Log("Throw");
            if (target is null) return;

            target.isKinematic = false;
            target.velocity = (player.RigidBody.rotation * Vector3.forward).normalized * ThrowSpeed;
            target = null;
        }
        else if (Input.GetMouseButtonUp(1)) // Drop Object
        {
            Debug.Log("Drop");
            if (target is null) return;

            target.isKinematic = false;
            target = null;
        }
        else if (Input.GetMouseButton(1)) // Carry Object
        {
            Debug.Log("Carry");
            if (target is null) return;

            target.transform.position =
                Vector3.MoveTowards(target.position, VisibleSphere.transform.position, MoveSpeed);
        }
        else // Drop Object
        {
            Debug.Log("Nothing");
            VisibleSphere.SetActive(false);
            
            if (target is null) return;
            target.isKinematic = false;
            target = null;
        }
    }
}