using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_UI_Responses : MonoBehaviour
{
    const string startLevel = "Room 1";
    const string characterDanceKey = "IsDancing";
    const string danceIndex_ParamName = "danceIndex";


    int zoomFov = 180;
    int defautFov = 60;
    float smoothFactor = 5f;
    bool clickedPlay;
    bool shouldBeInAboutSection;


    [SerializeField] Transform pos2;
    [SerializeField] Transform pos1;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip buttonSFX;
    [SerializeField] private GameObject uiHinge = null;
    [SerializeField] private Animator mainMenuCharacterAnimator = null;

    private void Start()
    {
        // THIS IS A HACK BECAUSE DEAD GUM LOADSCENE IS BLASTING UNPAUSE
        if(PauseMenuSingleton.Active != null)
            PauseMenuSingleton.Active.Unpause();
    }

    public void OnPlayClicked()
    {
        clickedPlay = true;
        Invoke("loadFirstLevel", 1);
    }

    void loadFirstLevel()
    {
        SceneManager.LoadScene("Global");
        SceneManager.LoadScene(startLevel, LoadSceneMode.Additive);
    }

    public void OnAbout_Clicked()
    {
        shouldBeInAboutSection = true;
    }

    public void OnAbout_Exit_Clicked()
    {
        shouldBeInAboutSection = false;
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
            return;
        }
        
        PollAboutSectionAndUpdate();

    }

    private void PollAboutSectionAndUpdate()
    {
        Transform txfmToRotate = uiHinge.transform;

        if(mainMenuCharacterAnimator != null)
        {
            if(shouldBeInAboutSection)
            {
                mainMenuCharacterAnimator.SetBool(characterDanceKey, true);
            }
            else
            {
                mainMenuCharacterAnimator.SetBool(characterDanceKey, false);
            }
        }

        if (shouldBeInAboutSection)
        {
            txfmToRotate.rotation = Quaternion.Lerp(txfmToRotate.rotation, pos2.rotation, Time.deltaTime * smoothFactor);
        }
        else
        {
            txfmToRotate.rotation = Quaternion.Lerp(txfmToRotate.rotation, pos1.rotation, Time.deltaTime * smoothFactor);
        }
    }


    public void SetDanceIndex(int index)
    {
        mainMenuCharacterAnimator.SetInteger(danceIndex_ParamName, index);
    }


}
