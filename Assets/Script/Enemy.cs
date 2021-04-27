using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * code panique
 * Diviser la partie waypoint / pas waypoint
 */
public class Enemy : MonoBehaviour
{
    // Varrible Statistique
    public float maxHealth;
    public float currentHealth;
    public int damage;
    public bool dead;
    public float timeBeforeInvok;
    public Animator animator;

    // Varriable patrol
    public float maxSpeed;
    public float speed;
    public Transform[] wayPoints;
    public Transform target;
    private int destinationPoint;
    public bool activated;
    public float timeBeforeMove;

    public Vector3 dir;

    public Room currentRoom;

    //Bubble  variable
    private GameObject bubble;
    public bool haveBubble;
    public bool destroyBubble;
    public Animator animatorBubble;

    public void SetUp()
    {
        StartCoroutine(SetUpCoroutine());  // on est rusé nous ;)
    }

    public IEnumerator SetUpCoroutine()
    {
        yield return new WaitForSeconds(timeBeforeInvok);
        animator.SetTrigger("Invok");
        yield return new WaitForSeconds(1f); //Temps de la petit roue qui tourne

        if (haveBubble)
        {
            bubble = Instantiate(Resources.Load(PrefabFinder.RessourcesToURI[Ressources.Bubble]) as GameObject, transform.position, Quaternion.identity);
            animatorBubble = bubble.GetComponent<Animator>();
            bubble.transform.parent = this.transform;
        }

        activated = true;
    }

    private void Update()
    {
        if (activated && !dead && timeBeforeMove ==0)
        {
            Patrol();
        }
    }

    private void FixedUpdate()
    {
        if (timeBeforeMove > 0 && activated)
        {
            timeBeforeMove -= Time.fixedDeltaTime;
        }
        else if(timeBeforeMove <= 0 && activated)
        {
            timeBeforeMove = 0;
        }
    }

    public void Patrol()
    {
        dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        //Si l'ennemi est quasiment arriver à destination
        if (Vector3.Distance(transform.position, target.position) < Constants.DESTINATION_NEARBY)
        {
            destinationPoint = (destinationPoint + 1) % wayPoints.Length;
            target = wayPoints[destinationPoint];
        }
    }

    /*
     * Désactive le gameobject et signifie à la pièce du monstre qu'on a tout nettoyé
     * */

    public IEnumerator Die()
    {
        dead = true;
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponentInChildren<SpriteRenderer>().enabled = false;
        currentRoom.notifyDeath();

        //[CODE REVIEW] Faire en sorte d'avoir le joueur responsable d'une explosion / meurtre / truc en mémoire
        // On peut faire ça par détection dans la pièce où on est

        //On fait la detection de quelle joueur à tuer le pauvre petit gugus
        GameObject detectionPlayer = Instantiate(Resources.Load(PrefabFinder.RessourcesToURI[Ressources.Detection_player]) as GameObject, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f); //comme ça find player a le temps de find mdr
        GameObject player = detectionPlayer.GetComponent<FindPlayer>().playerFind;

        ComboManager.instance.AddToCombo(player);
        
        // Et la boum boum
        Instantiate(Resources.Load(PrefabFinder.RessourcesToURI[Ressources.Explosion]) as GameObject, transform.position, Quaternion.identity);
    }

    // Provisoire
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {            
            if (!dead)
            {
                if(collision.GetComponent<PlayerHealth>().isInvincible == false)
                {
                    StartCoroutine(collision.GetComponent<PlayerHealth>().TakeDamage(dir, damage));                    
                }
            }
        }

        if (collision.CompareTag("Epée"))
        {
            if (!dead)
            {
                if(haveBubble && !destroyBubble)
                {
                    animatorBubble.SetTrigger("plop");
                    destroyBubble = true;
                }
                else
                {
                    StartCoroutine(Die());
                }
            }
        }

        if (collision.CompareTag("Explosion"))
        {
            if (!dead)
            {
                if (haveBubble && !destroyBubble)
                {
                    animatorBubble.SetTrigger("plop");
                    destroyBubble = true;
                }
                else
                {
                    StartCoroutine(Die());
                }
            }
        }
    }
}
