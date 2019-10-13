using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private MouseWorldPosition _mouseWorldPosition;
    [SerializeField] private PlayerAnimationCodeHook _playerAnimatorCodeHook = null;
    [SerializeField] private PlayerLifeController _lifeController = new PlayerLifeController();

    public static PlayerSingleton Active { get; private set; }

    public Rigidbody RigidBody => _rigidBody;

    public MouseWorldPosition MouseWorldPosition => _mouseWorldPosition;
    public PlayerAnimationCodeHook PlayerAnimationCodeHook => _playerAnimatorCodeHook;


    public bool IsDead { get { return _lifeController.IsDead; } }

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