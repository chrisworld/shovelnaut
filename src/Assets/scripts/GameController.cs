﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    private bool paused = false;
    private GameObject pauseText;
    // Start is called before the first frame update
    void Start()
    {
        pauseText = GameObject.FindWithTag("Pause");
        pauseText?.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bool controllerPause = Gamepad.current != null ? Gamepad.current.startButton.wasPressedThisFrame : false ;
        if(Input.GetKeyDown(KeyCode.Escape) || controllerPause)
        {
            if (!paused)
            {
                PauseGame();
            } else
            {
                ResumeGame();
            }
        }
        
    }

    public void PauseGame()
    {
        paused = !paused;
        Time.timeScale = 0;
        AudioListener.pause = paused;
        pauseText?.SetActive(paused);
    }

    public void ResumeGame()
    {
        paused = !paused;
        Time.timeScale = 1;
        AudioListener.pause = paused;
        pauseText?.SetActive(paused);
    }
}
