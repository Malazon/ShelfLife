using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject DeathParent;
    [SerializeField] private GameObject PauseParent;
    [SerializeField] private GameObject WonParent;
    [SerializeField] private AudioSource pauseSFXSource;
    [SerializeField] private AudioClip pauseSFXClip;

    private void OnEnable()
    {
        WonParent.SetActive(false);
        DeathParent.SetActive(false);
        PauseParent.SetActive(false);
        
        if (PauseMenuSingleton.Active.WonGame)
        {
            WonParent.SetActive(true);
        }
        else if (PlayerSingleton.Combatant != null && PlayerSingleton.Combatant.HasDied)
        {
            DeathParent.SetActive(true);
        }
        else
        {
            PauseParent.SetActive(true);
        }
    }

    public void playButtonSFX()
    {
        pauseSFXSource.PlayOneShot(pauseSFXClip);
    }

    public void callPlayerUnpause()
    {
        PauseMenuSingleton.Active.Unpause();
    }

    private void LoadMainMenu ()
    {
        PauseMenuSingleton.Active.Unpause();
        SceneManager.LoadSceneAsync("Main Menu");
    }

    private void Update()
    {

       if (Input.GetKeyUp(KeyCode.Escape))
        {
            
            if (PauseMenuSingleton.Active.WonGame)
            {
                LoadMainMenu();
            }
            else if (PlayerSingleton.Combatant != null && PlayerSingleton.Combatant.HasDied)
            {
                LoadMainMenu();
            }
            else
            {
                PauseMenuSingleton.Active.Unpause();
            }
        }
    }
}
