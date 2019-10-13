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

        var player = PlayerSingleton.Active;

        if (player.transform.position.y < -5)
        {
            player.Combatant.Kill();
            PauseMenuSingleton.Active.Pause();
        }
        
        if (player.Combatant.HasDied && hasTriggeredDeath)
        {
            PlayerSingleton.Active.PlayerAnimationCodeHook.SetDead(true);
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
