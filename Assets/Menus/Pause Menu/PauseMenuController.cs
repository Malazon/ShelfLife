using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject DeathParent;
    [SerializeField] private GameObject PauseParent;
    private void OnEnable()
    {
        if (PlayerSingleton.Active != null && PlayerSingleton.Active.Combatant.HasDied)
        {
            // Show the you died object
            PauseParent.SetActive(false);
            DeathParent.SetActive(true);
        }
        else
        {
            // Show the pause object
            PauseParent.SetActive(true);
            DeathParent.SetActive(false);
        }
    }
}
