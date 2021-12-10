using UnityEngine;

public class BoutonStart : MonoBehaviour
{
    public Animator animator;

    //Sound effect
    public AudioClip sound_button;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayClipAt(sound_button, transform.position);
            animator.SetBool("on", true);
            GameManager.instance.boutonsStart[this] = true;
            GameManager.instance.BoutonCheck();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("on", false);
            GameManager.instance.boutonsStart[this] = false;
        }
    }
}
