using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Varrible Statistique
    public float maxHealth;
    public float currentHealth;
    public float damage;
    public bool dead;
    public bool haveBubble;

    // Varriable patrol
    public float maxSpeed;
    public float speed;
    public Transform[] wayPoints;
    public Transform target;
    private int destinationPoint;
    public bool activated;
    public float timeBeforeMove;

    public Vector3 dir;

    // Varrible bubble
    public Room currentRoom;
    
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
    public void Die()
    {
        dead = true;
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponentInChildren<SpriteRenderer>().enabled = false;
        currentRoom.notifyDeath();
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

                //if(collision.GetComponent<PlayerMovement>().isBump == false)
                //{
                //    StartCoroutine(collision.GetComponent<PlayerMovement>().Bumping(dir));
                //}               

            }
        }

        if (collision.CompareTag("Epée"))
        {
            if (!dead)
            {
                Die();
            }
        }
    }
}
