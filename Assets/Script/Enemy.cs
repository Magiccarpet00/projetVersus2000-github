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
    
    public Animator animator;

    // Varriable patrol
    public float maxSpeed;
    public float speed;
    public Transform[] wayPoints;
    public Transform target;
    private int destinationPoint;
    public bool activated;
    private bool isStop;

    [Range(0f, 10f)]
    public float timeBeforeInvok;
    [Range(0f,10f)]
    public float timeBeforeMove;
    

    public Vector3 dir;

    public Room currentRoom;

    //Bubble  variable
    private GameObject bubble;
    public bool haveBubble;
    public bool destroyBubble;
    public Animator animatorBubble; // pour moi on devrait avoir bubble.animatorBubble (i say that I say nothing)

    private void Start()
    {
        target = wayPoints[0];
    }

    public void SetUp()
    {
        StartCoroutine(SetUpCoroutine());  // on est rusé nous ;)
    }

    public IEnumerator SetUpCoroutine()
    {
        yield return new WaitForSeconds(timeBeforeInvok);
        animator.SetTrigger("Invok");
        yield return new WaitForSeconds(Constants.ENEMY_INVOK_DURATION); //Temps de la petit roue qui tourne

        if (haveBubble)
        {
            bubble = Instantiate(Resources.Load(PrefabFinder.RessourcesToURI[Ressources.Bubble]) as GameObject, transform.position, Quaternion.identity);
            animatorBubble = bubble.GetComponent<Animator>();
            bubble.transform.parent = this.transform;
        }

        this.GetComponent<BoxCollider2D>().enabled = true;
        activated = true;
    }

    private void Update()
    {
        if (activated && !dead && timeBeforeMove ==0 && !isStop)
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
            if(target.GetComponent<Target>().stopTarget == true)
            {
                StartCoroutine(StopMove());
            }
            else
            {
                ChangeTarget();
            }            
        }
    }

    public IEnumerator StopMove()
    {
        isStop = true;

        float timeStop = target.GetComponent<Target>().timeStop;
        yield return new WaitForSeconds(timeStop);

        isStop = false;
        ChangeTarget();
    }

    public void ChangeTarget()
    {
        //Changement de target
        destinationPoint = (destinationPoint + 1) % wayPoints.Length;
        target = wayPoints[destinationPoint];
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

    private void OnTriggerEnter2D(Collider2D collision) //[Code review] ? mais pas sur pcq au final je trouve ça bien
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

        // c'est déjà plus compact ;-)
        if (collision.CompareTag("Epée") || collision.CompareTag("Explosion"))
        {
            if (!dead)
            {
                if(haveBubble && !destroyBubble)
                {
                    animatorBubble.SetTrigger("plop"); // what kind of name is this ?
                    destroyBubble = true;
                }
                else
                {
                    StartCoroutine(Die());
                }
            }
        }

        if (collision.CompareTag("Range_Attack"))
        {
            if (!dead)
            {
                if (haveBubble && !destroyBubble)
                {
                    animatorBubble.SetTrigger("plop"); // what kind of name is this ?
                    destroyBubble = true;

                    //Redonner une munition au joueur quand il touche la bubble (PROVISOIR)
                    GameManager.instance.players[0].GetComponent<PlayerInventory>().munitionRangeAttack++;
                    GameManager.instance.players[0].GetComponent<PlayerInventory>().UpdateUI();

                }
                else
                {
                    TakeDamage(1); //ok je suis un bébé...
                }

                Destroy(collision.gameObject);
            }
            
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        if(currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }
}
