using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    public int posCursor = 0;
    public GameObject[] cursor;

    public 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
            if(Input.GetKeyDown(KeyCode.S) ||
               Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveCursor(1);
            }

            if (Input.GetKeyDown(KeyCode.Z) ||
               Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveCursor(-1);
            }


            if (Input.GetKeyDown(KeyCode.B))
            {

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


    public void ReprendreBouton()
    {
        Resume();
    }

    public void RecommencerBouton()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitterBouton()
    {
        Application.Quit();
    }

}
