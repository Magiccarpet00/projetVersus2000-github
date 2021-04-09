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
        playerMovement.frezze = true;

        //[Annimation]
        animator.SetBool("receiveHit", true);

        StartCoroutine(playerMovement.Bumping(_bumpForce));

        yield return new WaitForSeconds(Constants.TIME_TO_HITSTUN); // Après être repousé on ne peut plus bouger pendant TIME TO HITSTUN secondes

        //Freeze le gugus
        playerMovement.frezze = false;

        //[Annimation]
        animator.SetBool("receiveHit", false); // Pour l'instant je triche pcq l'annime s'arrete pas au bon momment

        yield return new WaitForSeconds(Constants.TIME_INVINCIBLE_AFTER_HITSTUN);
        isInvincible = false;        
    }
}
