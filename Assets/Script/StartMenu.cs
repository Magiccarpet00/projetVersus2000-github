using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public Animator buttonStart;
    public Animator titleScreen;
    public AudioManager audioManager;

    //Sound effect    
    public AudioClip sound_start;


    private void Start()
    {
        
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            AudioManager.instance.PlayClipAt(sound_start, transform.position);
            StartCoroutine(LoadNextScene());
        }
    }

    public IEnumerator LoadNextScene()
    {    
        buttonStart.SetTrigger("go");
        titleScreen.SetTrigger("go");
        yield return new WaitForSeconds(1.4f);        
        SceneManager.LoadScene(1);        
    }

}
