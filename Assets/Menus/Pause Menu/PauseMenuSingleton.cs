﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PauseMenuSingleton : MonoBehaviour
{
    public static PauseMenuSingleton Active { get; private set; }
    [SerializeField] private GameObject PauseMenuPrefab;
    [SerializeField] private GameObject PauseMenuInstance;


    private bool _paused = false;
    public bool DisablePause = false;

    public static bool Paused
    {
        get
        {
            if (Active != null) return Active._paused;
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Active != null)
            if (Active.gameObject.activeInHierarchy)
            {
                DestroyImmediate(this);
                return;
            }

        Active = this;
    }

    public void Pause()
    {
        if (PauseMenuInstance == null)
        {
            PauseMenuInstance = GameObject.Instantiate(PauseMenuPrefab, null);
        }
        else
        {
            PauseMenuInstance.SetActive(true);
        }

        Time.timeScale = 0;
        _paused = true;
    }

    public void Unpause()
    {
        if (PauseMenuInstance != null)
        {
            PauseMenuInstance.SetActive(false);
        }

        Time.timeScale = 1;
        _paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (DisablePause) return;
        if (_paused) return;
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }
    }
}
