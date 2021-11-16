using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [code panique]
 * Diviser la partie waypoint / pas waypoint
 */
public class Enemy : MonoBehaviour
{
    // Varrible Statistique
    public float maxHealth;
    public float currentHealth;
    public int damage;
    public bool dead;
    public AudioClip sound_hitDeath;

    public Animator animator;
    public SpriteRenderer shadow;
    public CircleCollider2D hitbox;

    // Varriable patrol
    public float maxSpeed;
    public float speed;
    public Transform[] wayPoints;
    public Transform target;
    private int destinationPoint;
    public bool activated;
    public bool pre_activated;
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
    public Animator animatorBubble; // pour moi on devrait avoir bubble.animatorBubble



    private void Start()
    {
        target = wayPoints[0];
    }

    public void SetUp()
    {
        if (!pre_activated)
        {
            pre_activated = true;
            StartCoroutine(SetUpCoroutine());
        }        
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
            bubble.GetComponent<Bubble>().atachedEnemy = this.gameObject;
            destroyBubble = false;
        }
        else
        {
            destroyBubble = true;
        }

        hitbox.enabled = true;
        shadow.enabled = true;
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
        hitbox.enabled = false;
        this.GetComponentInChildren<SpriteRenderer>().enabled = false; //[Code Review] shadow et sprite trop similaire
        shadow.enabled = false;
        currentRoom.notifyDeath();
        PlaySoundHitDeath();

        //[CODE REVIEW] Faire en sorte d'avoir le joueur responsable d'une explosion / meurtre / truc en mémoire
        // On peut faire ça par détection dans la pièce où on est

        //On fait la detection de quelle joueur à tuer le petit gugus
        GameObject detectionPlayer = Instantiate(Resources.Load(PrefabFinder.RessourcesToURI[Ressources.Detection_player]) as GameObject, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f); //comme ça find player a le temps de find
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
                    if(collision.GetComponent<PlayerCharacter>().character == Character.RED)
                    {
                        if(collision.GetComponent<PlayerAttack>().isAttacking == true)
                        {
                            if (destroyBubble)
                            {
                                StartCoroutine(Die());
                            }
                            else
                            {
                                collision.GetComponent<PlayerAttack>().RedCloseAttackRetrit();
                            }
                        }
                        else
                        {
                            StartCoroutine(collision.GetComponent<PlayerHealth>().TakeDamage(dir, damage));
                        }
                    }
                    else if(collision.GetComponent<PlayerCharacter>().character == Character.BLUE)
                    {
                        StartCoroutine(collision.GetComponent<PlayerHealth>().TakeDamage(dir, damage));
                    }

                    else if(collision.GetComponent<PlayerCharacter>().character == Character.GREEN)
                    {

                    }
                }
            }
        }

        if (collision.CompareTag("Epée") || collision.CompareTag("Explosion") || collision.CompareTag("Range_Attack"))
        {
            if (!dead)
            {
                if (destroyBubble)
                {
                    StartCoroutine(Die());
                    if (collision.CompareTag("Range_Attack"))
                    {
                        Destroy(collision.gameObject);
                    }
                }
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

    public void PlaySoundHitDeath() {
        AudioManager.instance.PlayClipAt(sound_hitDeath, transform.position);
    }
}
