using System.Collections;
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
    public PlayerCharacter playerCharacter;


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
        playerCharacter = GetComponent<PlayerCharacter>();
    }

    public IEnumerator TakeDamage(Vector2 _bumpForce, int amount)
    {
        isInvincible = true;

        StartCoroutine(playerMovement.Bumping(_bumpForce));

        //Freeze le gugus
        playerMovement.checkSwitchBoxMove("isStunned", true);

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthUI();

        // [Code Review] reciveiveHit et die doivent toujours etre ecrit comme ça pour que l'animator de chaque perso comprennent
        animator.SetTrigger("receiveHit");

        if(playerCharacter.character == Character.BLUE) // [Code Review] C'est une rustine pcq sinon ya une bug d'annimation et de dépaclement à cause du slide
        {
            GetComponent<PlayerAttack>().isBufferingAttack = false;
            animator.SetBool("Button_Down", false);
            playerMovement.checkSwitchBoxMove("isBufferingAttack", false);
        }

        yield return new WaitForSeconds(Constants.TIME_TO_HITSTUN); // Après être repousé on ne peut plus bouger pendant TIME TO HITSTUN secondes

        //Fin freeze le gugus
        playerMovement.checkSwitchBoxMove("isStunned", false);

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
