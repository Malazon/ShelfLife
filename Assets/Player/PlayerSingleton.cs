using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private MouseWorldPosition _mouseWorldPosition;
    public static PlayerSingleton Active { get; private set; }

    public Rigidbody RigidBody => _rigidBody;

    public MouseWorldPosition MouseWorldPosition => _mouseWorldPosition;

    private void Start()
    {
        if (Active != null)
            if (Active.gameObject.activeInHierarchy)
            {
                Debug.LogWarning("Tried to initiate an additional player.");
                gameObject.SetActive(false);
                return;
            }

        Active = this;
    }
}