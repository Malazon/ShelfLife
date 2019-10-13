using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_UI_Responses : MonoBehaviour
{
    public void OnPlayClicked()
    {
        SceneManager.LoadScene("Global");
        SceneManager.LoadSceneAsync("PrettyTesting",LoadSceneMode.Additive);
    }

    public void OnAbout_Clicked()
    {
        // TODO:  Open About page!
        Debug.LogWarning("OnAbout_Clicked() called but not implemented yet!");
    }

    public void OnExit_Clicked()
    {
        Application.Quit();
    }
}
