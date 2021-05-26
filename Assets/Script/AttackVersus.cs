using System.Collections;
using UnityEngine;

public class AttackVersus : MonoBehaviour
{
    public GameObject playerToFocus;
    public GameObject player;

    //BLUE
    public Animator animator;

    private void Start()
    {
        if(player.GetComponent<PlayerCharacter>().character == Character.BLUE)
        {
            StartCoroutine(BlueAttackVersus());
        }

        if (player.GetComponent<PlayerCharacter>().character == Character.RED)
        {
            RedAttackVersus();
        }

        if (player.GetComponent<PlayerCharacter>().character == Character.GREEN)
        {
            GreenAttackVersus();
        }
    }

    public IEnumerator BlueAttackVersus()
    {
        animator = GetComponent<Animator>();
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("fire");
        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("end");
    }

    public void RedAttackVersus()
    {

    }

    public void GreenAttackVersus()
    {

    }

}
