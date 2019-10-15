using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private PlayerAnimationCodeHook playerAnimatorCodeHook = null;
    [SerializeField] private Combatant combatant;

    #endregion

    #region Static Accessors
    public static PlayerSingleton Active { get; private set; }
    public static Rigidbody RigidBody { get => Active == null ? null : Active.rigidBody; }
    public static Combatant Combatant { get => Active == null ? null : Active.combatant; }
    public static PlayerAnimationCodeHook PlayerAnimationCodeHook { get => Active == null ? null : Active.playerAnimatorCodeHook; }
    
    #endregion
    
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