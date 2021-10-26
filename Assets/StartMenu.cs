using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public Animator buttonStart;
    public Animator titleScreen;


    void Update()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine(LoadNextScene());
        }
    }

    public IEnumerator LoadNextScene()
    {
        buttonStart.SetTrigger("go");
        titleScreen.SetTrigger("go");
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(1);
    }
}
