using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public bool dead;

    public bool isInvincible;

    public Animator animator;
    public PlayerMovement playerMovement;


    //POUR LE CANVAS

    public int NumOfHeart;
    public Image[] heart;
    public Sprite fullHeart;
    public Sprite emptyHeart;


    private void Start()
    {
        currentHealth = maxHealth;
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
    }

    public IEnumerator TakeDamage(Vector2 _bumpForce, int amount)
    {
        isInvincible = true;

        StartCoroutine(playerMovement.Bumping(_bumpForce));

        //Freeze le gugus
        playerMovement.isStunned = true;
        playerMovement.checkSwitchBoxMove("isStunned", playerMovement.isStunned);

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthUI();

        //[Annimation]
        animator.SetTrigger("receiveHit");

        yield return new WaitForSeconds(Constants.TIME_TO_HITSTUN); // Après être repousé on ne peut plus bouger pendant TIME TO HITSTUN secondes

        //Freeze le gugus
        playerMovement.isStunned = false; //[BUG] ici on passe pas avec le debug en dirrait que unity se perd dans le waitForSeconds si dessus
        playerMovement.checkSwitchBoxMove("isStunned", playerMovement.isStunned);

        //[Annimation]
        //animator.SetBool("receiveHit", false); // Pour l'instant je triche pcq l'annime s'arrete pas au bon momment

        yield return new WaitForSeconds(Constants.TIME_INVINCIBLE_AFTER_HITSTUN);
        isInvincible = false;        
    }

    public void Die()
    {
        dead = true;
        animator.SetTrigger("die");
        this.GetComponent<CircleCollider2D>().enabled = false;
    }

    public void UpdateHealthUI()
    {
        for (int i = 0; i < heart.Length; i++)
        {
            if(i < currentHealth)
            {
                heart[i].sprite = fullHeart;
            }
            else
            {
                heart[i].sprite = emptyHeart;
            }
        }
    }
}
