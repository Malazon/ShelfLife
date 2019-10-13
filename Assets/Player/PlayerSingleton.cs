using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private MouseWorldPosition _mouseWorldPosition;
    [SerializeField] private PlayerAnimationCodeHook _playerAnimatorCodeHook = null;
    [SerializeField] private PlayerLifeController _lifeController;
    [SerializeField] private Combatant _combatant;

    public static PlayerSingleton Active { get; private set; }

    public Rigidbody RigidBody => _rigidBody;
    public Combatant Combatant => _combatant;

    public MouseWorldPosition MouseWorldPosition => _mouseWorldPosition;
    public PlayerAnimationCodeHook PlayerAnimationCodeHook => _playerAnimatorCodeHook;
    
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