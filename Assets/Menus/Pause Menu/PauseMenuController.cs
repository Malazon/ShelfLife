using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            
            if (PlayerSingleton.Active != null && PlayerSingleton.Active.Combatant.HasDied)
            {
                SceneManager.LoadScene("Main Menu");
            }
            else
            {
                PauseMenuSingleton.Active.Unpause();
            }
        }
    }
}
