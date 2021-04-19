using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public bool dead;

    public bool isInvincible;

    public Animator animator;
    public PlayerMovement playerMovement;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    public IEnumerator TakeDamage(Vector2 _bumpForce, float amount)
    {
        isInvincible = true;

        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Die();
        }

        //Freeze le gugus
        playerMovement.isStunned = true;
        playerMovement.checkSwitchBoxMove("isStunned", playerMovement.isStunned);

        //[Annimation]
        animator.SetBool("receiveHit", true);

        StartCoroutine(playerMovement.Bumping(_bumpForce));

        yield return new WaitForSeconds(Constants.TIME_TO_HITSTUN); // Après être repousé on ne peut plus bouger pendant TIME TO HITSTUN secondes

        //Freeze le gugus
        playerMovement.isStunned = false; //[BUG] ici on passe pas avec le debug en dirrait que unity se perd dans le waitForSeconds si dessus
        playerMovement.checkSwitchBoxMove("isStunned", playerMovement.isStunned);

        //[Annimation]
        animator.SetBool("receiveHit", false); // Pour l'instant je triche pcq l'annime s'arrete pas au bon momment

        yield return new WaitForSeconds(Constants.TIME_INVINCIBLE_AFTER_HITSTUN);
        isInvincible = false;        
    }

    public void Die()
    {
        dead = true;
        this.GetComponentInChildren<SpriteRenderer>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = false;
        
    }
}
