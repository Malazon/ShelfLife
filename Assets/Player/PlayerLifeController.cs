using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This should be updated to depend on a combatant controller on the player.
public class PlayerLifeController : MonoBehaviour
{
    #region Player Health
    [SerializeField] private int _maxHealth = 4;
    private int _currentHealth = 4;

    public void ChangeHealth(int deltaHealth)
    {
        if (PlayerSingleton.Active == null) return;

        var player = PlayerSingleton.Active;

        if (_currentHealth < 1 && deltaHealth > 0)
            player.PlayerAnimationCodeHook.SetDead(false);

        bool wasDead = IsDead;

        _currentHealth = Mathf.Clamp(_currentHealth + deltaHealth, 0, _maxHealth);

        if (!wasDead && IsDead)
        {
            DoDie();
        }
    }

    public bool IsDead { get { return _currentHealth < 1; } }

    private void DoDie()
    {
        // ded
        PlayerSingleton.Active.PlayerAnimationCodeHook.SetDead(true);
        Debug.Log("Played died.");
        // Could timeline animate in a game over sprite
    }

    // For testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeHealth(+1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeHealth(-1);
        }
    }

    #endregion
}
