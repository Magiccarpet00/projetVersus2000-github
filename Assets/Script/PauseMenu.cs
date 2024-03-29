﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    public int posCursor = 0;
    public GameObject[] cursor;
    public PlayerInput playerInput;
    public bool cdCursor = false;


    public static PauseMenu instance;
    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetButtonDown(playerInput.start)) )
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }


        if (gameIsPaused) // Les input quand le jeu est en pause
        {
            if( Input.GetAxis(playerInput.verticalAxeJoypad) == 1f ||
                Input.GetKeyDown(KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.S) )
            {                
                MoveCursor(1);                               
            }

            if ( Input.GetAxis(playerInput.verticalAxeJoypad) == -1f ||
                 Input.GetKeyDown(KeyCode.UpArrow) ||
                 Input.GetKeyDown(KeyCode.Z) )
            { 
                MoveCursor(-1);                
            }

            if (Input.GetButtonDown(playerInput.button0))
            {
                SelectBouton(posCursor);
            }

        }
    }

    public void Paused()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
          
    }

    public void MoveCursor(int movePos)
    {
        
        cursor[posCursor].SetActive(false);

        posCursor += movePos;

        if(posCursor == 3)
        {
            posCursor = 0;
        }

        if(posCursor == -1)
        {
            posCursor = 2;
        }

        cursor[posCursor].SetActive(true);

        
    }

    public void SelectBouton(int i)
    {
        switch (i)
        {
            case 0:
                ReprendreBouton();
                break;

            case 1:
                RecommencerBouton();
                break;

            case 2:
                QuitterBouton();
                break;                
        }


    }

    public void ReprendreBouton()
    {
        Resume();
    }

    public void RecommencerBouton()
    {
        Resume();
        SceneManager.LoadScene(1);
    }

    public void QuitterBouton()
    {
        Application.Quit();
    }

}
