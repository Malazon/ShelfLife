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

    public void LoadMainMenu ()
    {
        SceneManager.LoadScene("Main Menu");
        PauseMenuSingleton.Active.Unpause();
    }

    private void Update()
    {

       if (Input.GetKeyUp(KeyCode.Escape))
        {
            
            if (PauseMenuSingleton.Active.WonGame)
            {
                SceneManager.LoadScene("Main Menu");
                PauseMenuSingleton.Active.Unpause();
            } else if (PlayerSingleton.Combatant != null && PlayerSingleton.Combatant.HasDied)
            {
                SceneManager.LoadScene("Main Menu");
                PauseMenuSingleton.Active.Unpause();
            }
            else
            {
                PauseMenuSingleton.Active.Unpause();
            }
        }
    }
}
