using UnityEngine;

public class CameraSingleton : MonoBehaviour
{
    [SerializeField] private float moveRate = 5f;

    public Transform TargetLocation;
    [SerializeField] private float turnRate = 720f;
    [SerializeField] private Camera _camera;

    public static CameraSingleton Active { get; private set; }

    
    public Camera Camera => _camera;

    private void Start()
    {
        if (Active != null)
            if (Active.gameObject.activeInHierarchy)
            {
                Debug.LogWarning("Tried to initiate a second primary camera.");
                gameObject.SetActive(false);
                return;
            }

        Active = this;
        TargetLocation = transform;
    }

    private void Update()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, TargetLocation.position, moveRate * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetLocation.rotation,
            turnRate * Time.deltaTime);
    }
}