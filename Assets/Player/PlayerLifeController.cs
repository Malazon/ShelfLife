using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeController : MonoBehaviour
{
    private void Update()
    {
        if (PlayerSingleton.Active == null) return;

        var player = PlayerSingleton.Active;
        
        if (player.Combatant.HasDied)
        {
            PlayerSingleton.Active.PlayerAnimationCodeHook.SetDead(true);
            Debug.Log("Played died.");
        }
    }
}
