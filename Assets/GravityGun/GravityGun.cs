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

    private void Update()
    {
        if (PlayerSingleton.Active is null) return;
        var player = PlayerSingleton.Active;

        if (Input.GetMouseButtonDown(1)) // Pick up object
        {
            Debug.Log("Pick Up");

            focused.gameObject.SetActive(false);
            unfocused.gameObject.SetActive(true);

            if (target != null)
            {
                target.isKinematic = false;
                target = null;
            }

            VisibleSphere.SetActive(true);
            
            Debug.DrawRay(this.transform.position, Vector3.up, Color.blue, 5f);

            var raycastHits = Physics.SphereCastAll(this.transform.position - Vector3.up * this.transform.position.y, 2.5f,
                player.RigidBody.rotation * Vector3.forward, 4.5f, LayerMask.GetMask("Grabable"));

            float targetDistance = 10000f;

            foreach (var hit in raycastHits)
            {
                var collider = hit.collider;
                
                if (!collider.TryGetComponent(out Rigidbody colliderRigidBody)) continue;
                
                colliderRigidBody.AddRelativeForce(player.RigidBody.rotation * Vector3.back * BotherForce, ForceMode.Impulse);

                var colliderDistance = hit.distance;

                if (target is null || collider.gameObject.CompareTag("Grabable") || colliderDistance < targetDistance)
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
                Debug.DrawRay(target.position, Vector3.up, Color.cyan, 5f);
            }
        }
        else if (Input.GetMouseButtonDown(0)) // Throw Object
        {
            Debug.Log("Throw");
            if (target is null) return;

            focused.gameObject.SetActive(false);
            unfocused.gameObject.SetActive(true);

            target.isKinematic = false;
            target.velocity = (player.RigidBody.rotation * Vector3.forward).normalized * ThrowSpeed;
            target = null;
        }
        else if (Input.GetMouseButtonUp(1)) // Drop Object
        {
            Debug.Log("Drop");
            if (target is null) return;

            focused.gameObject.SetActive(false);
            unfocused.gameObject.SetActive(false);

            target.isKinematic = false;
            target = null;
        }
        else if (Input.GetMouseButton(1)) // Carry Object
        {
            Debug.Log("Carry");
            if (target is null) return;

            focused.gameObject.SetActive(true);
            unfocused.gameObject.SetActive(false);

            target.transform.position =
                Vector3.MoveTowards(target.position, this.transform.position, MoveSpeed * Time.deltaTime);
        }
        else // Drop Object
        {
            Debug.Log("Nothing");
            VisibleSphere.SetActive(false);

            focused.gameObject.SetActive(false);
            unfocused.gameObject.SetActive(false);

            if (target is null) return;
            target.isKinematic = false;
            target = null;
        }
    }
}