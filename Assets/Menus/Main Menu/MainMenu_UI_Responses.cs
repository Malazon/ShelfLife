using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_UI_Responses : MonoBehaviour
{

    int zoomFov = 180;
    int defautFov = 60;
    float smoothFactor = 5f;
    bool clickedPlay;
    bool changedPos;


    [SerializeField] Transform pos2;
    [SerializeField] Transform pos1;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip buttonSFX;

    public void OnPlayClicked()
    {
        clickedPlay = true;
        Invoke("loadFirstLevel", 1);
    }

    void loadFirstLevel()
    {
        SceneManager.LoadScene("Global");
        SceneManager.LoadSceneAsync("PrettyTesting",LoadSceneMode.Additive);
    }

    public void OnAbout_Clicked()
    {
        // TODO:  Open About page!
        Debug.LogWarning("OnAbout_Clicked() called but not implemented yet!");
        changedPos = true;
    }

    public void OnAbout_Exit_Clicked()
    {
        changedPos = false;
    }

    public void OnExit_Clicked()
    {
        Application.Quit();
    }

    public void playButtonSFX()
    {
        source.PlayOneShot(buttonSFX);
    }

    private void Update()
    {
        if(clickedPlay)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoomFov, Time.deltaTime * smoothFactor);
        }

        if(changedPos)
        {
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, pos2.rotation, Time.deltaTime * smoothFactor);
        }
        else
        {
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, pos1.rotation, Time.deltaTime * smoothFactor);
        }
    }


}
