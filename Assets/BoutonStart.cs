using UnityEngine;

public class BoutonStart : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("on", true);
        GameManager.instance.boutonsStart[this] = true;

        GameManager.instance.BoutonCheck();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("on", false);
        GameManager.instance.boutonsStart[this] = false;
    }
}
