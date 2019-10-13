using UnityEngine;

public class GravityGun : MonoBehaviour
{
    [SerializeField] private GameObject VisibleSphere;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float BotherForce;
    [SerializeField] private float ThrowSpeed;

    [SerializeField] private ParticleSystem unfocused;
    [SerializeField] private ParticleSystem focused;

    private Rigidbody target;

    private void tryResetTarget()
    {
        
        if (target != null)
        {
            target.useGravity = false;
            target.velocity = Vector3.zero;
            target = null;
        }
    }
    
    private void Update()
    {
        if (PlayerSingleton.Active == null) return;
        var player = PlayerSingleton.Active;

        if (Input.GetMouseButtonDown(1)) // Pick up object
        {
            //Debug.Log("Pick Up");

            focused.gameObject.SetActive(false);
            unfocused.gameObject.SetActive(true);

            tryResetTarget();
            
            VisibleSphere.SetActive(true);
            
            Debug.DrawRay(this.transform.position, Vector3.up, Color.blue, 5f);

            var raycastHits = Physics.SphereCastAll(this.transform.position - Vector3.up * this.transform.position.y, 1f,
                player.RigidBody.rotation * Vector3.forward, 3.5f, LayerMask.GetMask("Grabable"));

            float targetDistance = 10000f;
            int targetPriority = -1;

            foreach (var hit in raycastHits)
            {
                var collider = hit.collider;
                
                if (!collider.TryGetComponent(out Rigidbody colliderRigidBody)) continue;
                
                colliderRigidBody.AddRelativeForce(player.RigidBody.rotation * Vector3.back * BotherForce, ForceMode.Impulse);

                var colliderPriority = -1;
                
                if (collider.TryGetComponent(out GrabableObject colliderGrabbableObject)) colliderPriority = colliderGrabbableObject.Priority;
                
                var colliderDistance = hit.distance;

                if (target == null || collider.gameObject.CompareTag("Grabable"))
                {
                    if (target == null)
                    {
                        target = colliderRigidBody;
                        targetDistance = colliderDistance;
                        targetPriority = colliderPriority;
                    }
                    else if (colliderPriority > targetPriority)
                    {
                        target = colliderRigidBody;
                        targetDistance = colliderDistance;
                        targetPriority = colliderPriority;
                        
                    } else if (colliderPriority == targetPriority && colliderDistance < targetDistance)
                    {
                        target = colliderRigidBody;
                        targetDistance = colliderDistance;
                    }
                }
            }

            if (target == null)
            {
                VisibleSphere.SetActive(true);
            }
            else
            {
                target.useGravity = false;
                VisibleSphere.SetActive(false);
                Debug.DrawRay(target.position, Vector3.up, Color.cyan, 5f);
            }
        }
        else if (Input.GetMouseButtonDown(0)) // Throw Object
        {
            //Debug.Log("Throw");
            if (target == null) return;

            focused.gameObject.SetActive(false);
            unfocused.gameObject.SetActive(true);

            // Dont use reset here to set velocity manually.
            target.useGravity = true;
            target.velocity = (player.RigidBody.rotation * Vector3.forward).normalized * ThrowSpeed;
            target = null;
        }
        else if (Input.GetMouseButtonUp(1)) // Drop Object
        {
            //Debug.Log("Drop");
            if (target == null) return;

            focused.gameObject.SetActive(false);
            unfocused.gameObject.SetActive(false);

            tryResetTarget();
        }
        else if (Input.GetMouseButton(1)) // Carry Object
        {
            //Debug.Log("Carry");
            if (target == null) return;

            focused.gameObject.SetActive(true);
            unfocused.gameObject.SetActive(false);

            var holdPosition = this.transform.position + Vector3.down;
            var stepPosition = 
                Vector3.MoveTowards(target.position, holdPosition, MoveSpeed * Time.deltaTime);
            
            target.MovePosition(stepPosition);
            target.transform.rotation = this.transform.rotation;
        }
        else // Drop Object
        {
            //Debug.Log("Nothing");
            VisibleSphere.SetActive(false);

            focused.gameObject.SetActive(false);
            unfocused.gameObject.SetActive(false);

            tryResetTarget();
        }
    }
}