using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeController : MonoBehaviour
{
    private bool hasTriggeredDeath = false;
    private float deathStart = 0;
    private void Update()
    {
        if (PauseMenuSingleton.Paused) return;
        if (PlayerSingleton.Active == null) return;

        var playerCombatant = PlayerSingleton.Combatant;

        if (playerCombatant.transform.position.y < -5)
        {
            playerCombatant.Kill();
            PauseMenuSingleton.Active.Pause();
        }
        
        if (playerCombatant.HasDied && !hasTriggeredDeath)
        {
            PlayerSingleton.PlayerAnimationCodeHook.SetDead(true);
            
            if (PauseMenuSingleton.Active == null) return;
            PauseMenuSingleton.Active.DisablePause = true;
            deathStart = Time.time;
            hasTriggeredDeath = true;
        }

        if (hasTriggeredDeath && Time.time - deathStart > 5)
        {
            PauseMenuSingleton.Active.Pause();
        }
    }
}
