using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public bool isInvincible;

    public Animator animator;
    public PlayerMovement playerMovement;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public IEnumerator TakeDamage(Vector2 _bumpForce)
    {
        isInvincible = true;
        //appliquer les dégats...

        //Freeze le gugus
        playerMovement.StopMovement();

        //[Annimation]
        animator.SetBool("isDamageHit", true);

        StartCoroutine(playerMovement.Bumping(_bumpForce));

        yield return new WaitForSeconds(Constants.TIME_TO_HITSTUN);

        //Freeze le gugus
        playerMovement.UnStopMovement();

        //[Annimation]
        animator.SetBool("isDamageHit", false); // Pour l'instant je triche pcq l'annime s'arrete pas au bon momment

        yield return new WaitForSeconds(Constants.TIME_INVINCIBLE_AFTER_HITSTUN);
        isInvincible = false;        
    }
}
